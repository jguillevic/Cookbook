using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class RecipeKindServiceClient
    {
        private static string _url;

        static RecipeKindServiceClient()
        {
            _url = "http://localhost:51875/api/v1/recipeKinds";
        }

        public async static Task<List<RecipeKind>> LoadAsync()
        {
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<RecipeKind>>(_url);

            return values;
        }
    }
}
