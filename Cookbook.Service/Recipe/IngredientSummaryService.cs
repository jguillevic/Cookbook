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
    public class IngredientSummaryService
    {
        private static IngredientSummaryBLL _ingredientSummaryBLL;

        static IngredientSummaryService()
        {
            _ingredientSummaryBLL = new IngredientSummaryBLL();
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
                var ids = GetIngredientSummaryIds(context);

                List<IngredientSummary> ingredientSummaries;

                if (ids.Count > 0)
                    ingredientSummaries = _ingredientSummaryBLL.Load(new IngredientFilter { IdsToLoad = ids });
                else
                    ingredientSummaries = _ingredientSummaryBLL.Load();

                using (var stream = JsonHelper.SerializeToStream(ingredientSummaries))
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

        private static List<Guid> GetIngredientSummaryIds(HttpListenerContext context)
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
