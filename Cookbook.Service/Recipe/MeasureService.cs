using Cookbook.BLL.Recipe;
using Cookbook.Entity.Recipe;
using Cookbook.Serializer.Recipe.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Tools.Helper.Compress;
using Tools.Service.Http;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

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
                var filter = GetFilter(context);
                var fields = GetFields(context);

                var measures = _measureBLL.Load(filter, fields);

                var serializer = new MeasureJsonSerializer();
                serializer.SetFields(fields);

                using (var stream = serializer.Serialize(measures))
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

        private static MeasureFilter GetFilter(HttpListenerContext context)
        {
            var filter = new MeasureFilter();

            foreach (var key in context.Request.QueryString.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "id":
                        filter.IdsToLoad.Add(new Guid(context.Request.QueryString[key]));
                        break;
                    default:
                        break;
                }
            }

            return filter;
        }

        private static List<string> GetFields(HttpListenerContext context)
        {
            var fields = new List<string>();

            foreach (var key in context.Request.QueryString.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "field":
                        foreach (var field in context.Request.QueryString[key].Split(','))
                            fields.Add(field.ToLower());
                        break;
                    default:
                        break;
                }
            }

            if (fields.Count > 0)
                return fields;
            else
                return new List<string>(MeasureEntityDescription.AllLower);
        }

        private static void Add(HttpListenerContext context)
        {
            if (context.IsContentGZipJson()
                && context.Request.ContentLength64 > 0)
            {
                using (var gzip = GZipHelper.Decompress(context.Request.InputStream))
                {
                    var measures = new MeasureJsonSerializer().Deserialize(gzip);

                    if (measures.Count > 0)
                    {
                        _measureBLL.Add(measures); ;
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
                    var measures = new MeasureJsonSerializer().Deserialize(gzip);

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
