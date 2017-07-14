using Cookbook.Entity.Recipe;
using Cookbook.Serializer.Recipe.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class IngredientServiceClient
    {
        private static string _url;

        static IngredientServiceClient()
        {
            _url = "http://localhost:51875/api/v1/ingredients";
        }

        public async static Task<List<Ingredient>> LoadAsync()
        {
            return await LoadAsync(new IngredientFilter(), new List<string>());
        }

        public async static Task<List<Ingredient>> LoadAsync(IngredientFilter filter)
        {
            return await LoadAsync(filter, new List<string>());
        }

        public async static Task<List<Ingredient>> LoadAsync(List<string> fields)
        {
            return await LoadAsync(new IngredientFilter(), fields);
        }

        public async static Task<List<Ingredient>> LoadAsync(IngredientFilter filter, List<string> fields)
        {
            var sb = new StringBuilder();
            sb.Append(_url);

            bool isFirstParam = true;
            isFirstParam = AppendIngredientIds(filter.IdsToLoad, isFirstParam, sb);
            isFirstParam = AppendFields(fields, isFirstParam, sb);

            var serializer = new IngredientJsonSerializer();
            serializer.SetFields(fields);

            var values = await ServiceClientHelper.GetGzipJsonAsync(sb.ToString(), serializer);

            return values;
        }

        private static bool AppendIngredientIds(List<Guid> ingredientIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                ingredientIds
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "id={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendFields(List<string> fields, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                fields
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "field={0}", item)); }
                , isFirstParam
                , sb);
        }

        public async static Task<bool> AddAsync(List<Ingredient> ingredients)
        {
            return await ServiceClientHelper.PostGzipJsonAsync(_url, new IngredientJsonSerializer(), ingredients);
        }

        public async static Task<bool> UpdateAsync(List<Ingredient> ingredients)
        {
            return await ServiceClientHelper.PutGzipJsonAsync(_url, new IngredientJsonSerializer(), ingredients);
        }
    }
}
