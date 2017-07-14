using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;
using Cookbook.Serializer.Recipe.Json;

namespace Cookbook.ServiceClient.Recipe
{
    using Entity.Recipe;

    public static class RecipeServiceClient
    {
        private static string _url;

        static RecipeServiceClient()
        {
            // TODO : Sortir l'url dans un fichier de config.
            _url = "http://localhost:51875/api/v1/recipes";
        }

        public async static Task<List<Recipe>> LoadAsync()
        {
            return await LoadAsync(new RecipeFilter(), new List<string>());
        }

        public async static Task<List<Recipe>> LoadAsync(RecipeFilter filter)
        {
            return await LoadAsync(filter, new List<string>());
        }

        public async static Task<List<Recipe>> LoadAsync(List<string> fields)
        {
            return await LoadAsync(new RecipeFilter(), fields);
        }

        public async static Task<List<Recipe>> LoadAsync(RecipeFilter filter, List<string> fields)
        {
            var sb = new StringBuilder();
            sb.Append(_url);

            bool isFirstParam = true;
            isFirstParam = AppendRecipeIds(filter.IdsToLoad, isFirstParam, sb);
            isFirstParam = AppendFields(fields, isFirstParam, sb);
            isFirstParam = AppendName(filter.Name, isFirstParam, sb);
            isFirstParam = AppendCostIds(filter.CostIds, isFirstParam, sb);
            isFirstParam = AppendDifficultyIds(filter.DifficultyIds, isFirstParam, sb);
            isFirstParam = AppendRecipeKindIds(filter.RecipeKindIds, isFirstParam, sb);
            isFirstParam = AppendSeasonIds(filter.SeasonIds, isFirstParam, sb);
            isFirstParam = AppendFeatureIds(filter.FeatureIds, isFirstParam, sb);
            isFirstParam = AppendCookingTime(filter.CookingTime, isFirstParam, sb);
            isFirstParam = AppendPreparationTime(filter.PreparationTime, isFirstParam, sb);

            var serializer = new RecipeJsonSerializer();
            serializer.SetFields(fields);

            var values = await ServiceClientHelper.GetGzipJsonAsync(sb.ToString(), serializer);

            return values;
        }

        private static bool AppendRecipeIds(List<Guid> recipeIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                recipeIds
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

        private static bool AppendName(string name, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                name
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "name={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendCostIds(List<Guid> costIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                costIds
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "costid={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendDifficultyIds(List<Guid> difficultyIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                difficultyIds
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "difficultyid={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendRecipeKindIds(List<Guid> recipeKindIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                recipeKindIds
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "recipekindid={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendSeasonIds(List<Guid> seasonIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                seasonIds
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "seasonid={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendFeatureIds(List<Guid> featureIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                featureIds
                , item => { sb.Append(sb.Append(string.Format(CultureInfo.CurrentCulture, "featureid={0}", item))); }
                , isFirstParam
                , sb);
        }

        private static bool AppendCookingTime(int? cookingTime, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                cookingTime
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "cookingtime={0}", item)); }
                , isFirstParam
                , sb);
        }

        private static bool AppendPreparationTime(int? prepTime, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                prepTime
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "preparationtime={0}", item)); }
                , isFirstParam
                , sb);
        }

        public async static Task<bool> AddAsync(List<Recipe> recipes)
        {
            return await ServiceClientHelper.PostGzipJsonAsync(_url, new RecipeJsonSerializer(), recipes);
        }

        public async static Task<bool> UpdateAsync(List<Recipe> recipes)
        {
            return await ServiceClientHelper.PutGzipJsonAsync(_url, new RecipeJsonSerializer(), recipes);
        }
    }
}
