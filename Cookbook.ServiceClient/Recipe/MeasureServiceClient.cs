using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class MeasureServiceClient
    {
        private static string _url;

        static MeasureServiceClient()
        {
            _url = "http://localhost:51875/api/v1/measures";
        }

        public async static Task<List<Measure>> LoadAsync()
        {
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Measure>>(_url);

            return values;
        }

        //public async static bool Add(List<Measure> measures)
        //{
        //    return false;
        //}

        //public async static bool Update(List<Measure> measures)
        //{
        //    return false;
        //}
    }
}
