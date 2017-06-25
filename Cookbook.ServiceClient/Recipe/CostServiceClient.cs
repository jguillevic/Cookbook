﻿using Cookbook.Entity.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.ServiceClient.Helper;

namespace Cookbook.ServiceClient.Recipe
{
    public static class CostServiceClient
    {
        private static string _url;

        static CostServiceClient()
        {
            _url = "http://localhost:51875/api/v1/costs";
        }

        public async static Task<List<Cost>> LoadAsync()
        {
            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Cost>>(_url);

            return values;
        }
    }
}
