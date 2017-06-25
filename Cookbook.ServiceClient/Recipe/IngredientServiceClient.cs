using Cookbook.Entity.Recipe;
using System.Collections.Generic;
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
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Ingredient>>(_url);

            return values;
        }

        //public async static bool Add(List<Ingredient> ingredients)
        //{
        //    return false;
        //}

        //public async static bool Update(List<Ingredient> ingredients)
        //{
        //    return false;
        //}
    }
}
