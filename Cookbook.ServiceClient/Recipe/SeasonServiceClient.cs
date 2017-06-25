using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class SeasonServiceClient
    {
        private static string _url;

        static SeasonServiceClient()
        {
            _url = "http://localhost:51875/api/v1/seasons";
        }

        public async static Task<List<Season>> LoadAsync()
        {
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Season>>(_url);

            return values;
        }
    }
}
