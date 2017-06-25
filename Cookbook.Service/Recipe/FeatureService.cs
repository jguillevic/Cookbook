﻿using Cookbook.BLL.Recipe;
using System.Net;
using Tools.Helper.Compress;
using Tools.Helper.Json;
using Tools.Service.Http;

namespace Cookbook.Service.Recipe
{
    public static class FeatureService
    {
        private static FeatureBLL _featureBLL;

        static FeatureService()
        {
            _featureBLL = new FeatureBLL();
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
                var seasons = _featureBLL.Load();

                using (var stream = JsonHelper.SerializeToStream(seasons))
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
    }
}