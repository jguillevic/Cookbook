using Cookbook.BLL.Recipe;
using Cookbook.Entity.Recipe;
using System;
using System.Collections.Generic;
using System.Net;
using Tools.Helper.Compress;
using Tools.Helper.Json;
using Tools.Service.Http;

namespace Cookbook.Service.Recipe
{
    public class MeasureSummaryService
    {
        private static MeasureSummaryBLL _measureSummaryBLL;

        static MeasureSummaryService()
        {
            _measureSummaryBLL = new MeasureSummaryBLL();
        }

        public static void Process(HttpListenerContext context)
        {
            switch (context.Request.HttpMethod)
            {
                case HttpMethod.Get:
                    Load(context);
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
                var ids = GetMeasureSummaryIds(context);

                List<MeasureSummary> measureSummaries;

                if (ids.Count > 0)
                    measureSummaries = _measureSummaryBLL.Load(new MeasureFilter { IdsToLoad = ids });
                else
                    measureSummaries = _measureSummaryBLL.Load();

                using (var stream = JsonHelper.SerializeToStream(measureSummaries))
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

        private static List<Guid> GetMeasureSummaryIds(HttpListenerContext context)
        {
            var measureIds = new List<Guid>();

            foreach (var key in context.Request.QueryString.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "id":
                        measureIds.Add(new Guid(context.Request.QueryString[key]));
                        break;
                    default:
                        break;
                }
            }

            return measureIds;
        }
    }
}
