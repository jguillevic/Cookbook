using Cookbook.BLL.Recipe;
using Cookbook.Serializer.Recipe.Json;
using System.Collections.Generic;
using System.Net;
using Tools.Helper.Compress;
using Tools.Helper.Json;
using Tools.Service.Http;
using static Cookbook.Entity.Recipe.RecipeEntityDescriptions;

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
                var fields = GetFields(context);

                var features = _featureBLL.Load(fields);

                var serializer = new FeatureJsonSerializer();
                serializer.SetFields(fields);

                using (var stream = serializer.Serialize(features))
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
                return new List<string>(FeatureEntityDescription.AllLower);
        }
    }
}
