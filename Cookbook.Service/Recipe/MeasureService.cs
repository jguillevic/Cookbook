using Cookbook.BLL.Recipe;
using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Net;
using Tools.Helper.Compress;
using Tools.Helper.Json;
using Tools.Service.Http;

namespace Cookbook.Service.Recipe
{
    public static class MeasureService
    {
        private static MeasureBLL _measureBLL;

        static MeasureService()
        {
            _measureBLL = new MeasureBLL();
        }

        public static void Process(HttpListenerContext context)
        {
            switch (context.Request.HttpMethod)
            {
                case HttpMethod.Get:
                    Load(context);
                    break;
                case HttpMethod.Post:
                    Add(context);
                    break;
                case HttpMethod.Put:
                    Update(context);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    break;
            }
        }

        private static void Load(HttpListenerContext context)
        {
            if (context.IsAcceptGZipJson())
            {
                var measures = _measureBLL.Load();

                using (var stream = JsonHelper.SerializeToStream(measures))
                {
                    using (var gzip = GZipHelper.Compress(stream))
                    {
                        gzip.CopyTo(context.Response.OutputStream);
                    }
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                // TODO : Indiquer pourquoi.
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            }
        }

        private static void Add(HttpListenerContext context)
        {
            if (context.IsContentGZipJson()
                && context.Request.ContentLength64 > 0)
            {
                using (var gzip = GZipHelper.Decompress(context.Request.InputStream))
                {
                    var measures = JsonHelper.DeserializeFromStream<List<Measure>>(gzip);

                    if (measures.Count > 0)
                    {
                        _measureBLL.Add(measures);
                    }
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                // TODO : Indiquer pourquoi.
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private static void Update(HttpListenerContext context)
        {
            if (context.IsContentGZipJson()
                && context.Request.ContentLength64 > 0)
            {
                using (var gzip = GZipHelper.Decompress(context.Request.InputStream))
                {
                    var measures = JsonHelper.DeserializeFromStream<List<Measure>>(gzip);

                    if (measures.Count > 0)
                    {
                        _measureBLL.Update(measures);
                    }
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                // TODO : Indiquer pourquoi.
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
