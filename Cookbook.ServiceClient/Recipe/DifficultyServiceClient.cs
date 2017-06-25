using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class DifficultyServiceClient
    {
        private static string _url;

        static DifficultyServiceClient()
        {
            _url = "http://localhost:51875/api/v1/difficulties";
        }

        public async static Task<List<Difficulty>> LoadAsync()
        {
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Difficulty>>(_url);

            return values;
        }
    }
}
