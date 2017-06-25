using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class FeatureServiceClient
    {
        private static string _url;

        static FeatureServiceClient()
        {
            _url = "http://localhost:51875/api/v1/features";
        }

        public async static Task<List<Feature>> LoadAsync()
        {
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Feature>>(_url);

            return values;
        }
    }
}
