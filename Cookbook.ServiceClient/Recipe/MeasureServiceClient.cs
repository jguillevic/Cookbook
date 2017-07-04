using Cookbook.Entity.Recipe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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

        public async static Task<List<Measure>> LoadAsync(List<Guid> measureIds)
        {
            var sb = new StringBuilder();
            sb.Append(_url);

            bool isFirstParam = true;
            isFirstParam = AppendMeasureIds(measureIds, isFirstParam, sb);

            var values = await ServiceClientHelper.GetGzipJsonAsync<List<Measure>>(sb.ToString());

            return values;
        }

        private static bool AppendMeasureIds(List<Guid> measureIds, bool isFirstParam, StringBuilder sb)
        {
            if (measureIds != null && measureIds.Count > 0)
            {
                for (int i = 0; i < measureIds.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    sb.Append(string.Format(CultureInfo.CurrentCulture, "id={0}", measureIds[i]));
                }

                return false;
            }

            return isFirstParam;
        }

        public async static Task<bool> AddAsync(List<Measure> measures)
        {
            return await ServiceClientHelper.PostGzipJsonAsync(_url, measures);
        }

        public async static Task<bool> UpdateAsync(List<Measure> measures)
        {
            return await ServiceClientHelper.PutGzipJsonAsync(_url, measures);
        }
    }
}
