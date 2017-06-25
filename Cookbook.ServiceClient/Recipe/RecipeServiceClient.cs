using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

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

        public async static Task<List<Recipe>> LoadAsync(RecipeFilter filter)
        {
            var sb = new StringBuilder();
            sb.Append(_url);

            bool isFirstParam = true;
            isFirstParam = AppendName(filter.Name, isFirstParam, sb);
            isFirstParam = AppendCostIds(filter.CostIds, isFirstParam, sb);
            isFirstParam = AppendDifficultyIds(filter.DifficultyIds, isFirstParam, sb);
            isFirstParam = AppendRecipeKindIds(filter.RecipeKindIds, isFirstParam, sb);
            isFirstParam = AppendSeasonIds(filter.SeasonIds, isFirstParam, sb);
            isFirstParam = AppendFeatureIds(filter.FeatureIds, isFirstParam, sb);
            isFirstParam = AppendCookingTime(filter.CookingTime, isFirstParam, sb);
            isFirstParam = AppendPreparationTime(filter.PreparationTime, isFirstParam, sb);

            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Recipe>>(sb.ToString());

            return values;
        }

        private static bool AppendName(string name, bool isFirstParam, StringBuilder sb)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (isFirstParam)
                    sb.Append("?");
                else
                    sb.Append("&");

                sb.Append(string.Format(CultureInfo.CurrentCulture, "name={0}", name));

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendCostIds(List<Guid> costIds, bool isFirstParam, StringBuilder sb)
        {
            if (costIds != null && costIds.Count > 0)
            {
                for (int i = 0; i < costIds.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    sb.Append(string.Format(CultureInfo.CurrentCulture, "costIds={0}", costIds[i]));
                }

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendDifficultyIds(List<Guid> difficultyIds, bool isFirstParam, StringBuilder sb)
        {
            if (difficultyIds != null && difficultyIds.Count > 0)
            {
                for (int i = 0; i < difficultyIds.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    sb.Append(string.Format(CultureInfo.CurrentCulture, "difficultyIds={0}", difficultyIds[i]));
                }

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendRecipeKindIds(List<Guid> recipeKindIds, bool isFirstParam, StringBuilder sb)
        {
            if (recipeKindIds != null && recipeKindIds.Count > 0)
            {
                for (int i = 0; i < recipeKindIds.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    sb.Append(string.Format(CultureInfo.CurrentCulture, "recipeKindIds={0}", recipeKindIds[i]));
                }

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendSeasonIds(List<Guid> seasonIds, bool isFirstParam, StringBuilder sb)
        {
            if (seasonIds != null && seasonIds.Count > 0)
            {
                for (int i = 0; i < seasonIds.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    sb.Append(string.Format(CultureInfo.CurrentCulture, "seasonIds={0}", seasonIds[i]));
                }

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendFeatureIds(List<Guid> featureIds, bool isFirstParam, StringBuilder sb)
        {
            if (featureIds != null && featureIds.Count > 0)
            {
                for (int i = 0; i < featureIds.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    sb.Append(string.Format(CultureInfo.CurrentCulture, "featureIds={0}", featureIds[i]));
                }

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendCookingTime(int? cookingTime, bool isFirstParam, StringBuilder sb)
        {
            if (cookingTime.HasValue)
            {
                if (isFirstParam)
                    sb.Append("?");
                else
                    sb.Append("&");

                sb.Append(string.Format(CultureInfo.CurrentCulture, "cookingTime={0}", cookingTime));

                return false;
            }

            return isFirstParam;
        }

        private static bool AppendPreparationTime(int? prepTime, bool isFirstParam, StringBuilder sb)
        {
            if (prepTime.HasValue)
            {
                if (isFirstParam)
                    sb.Append("?");
                else
                    sb.Append("&");

                sb.Append(string.Format(CultureInfo.CurrentCulture, "preparationTime={0}", prepTime));

                return false;
            }

            return isFirstParam;
        }

        public async static Task<bool> AddAsync(IEnumerable<Recipe> recipes)
        {
            return await ServiceClientHelper.PostGzipJsonAsync(_url, recipes);
        }

        public async static Task<bool> UpdateAsync(IEnumerable<Recipe> recipes)
        {
            return await ServiceClientHelper.PutGzipJsonAsync(_url, recipes);
        }
    }
}
