using Cookbook.Entity.Recipe;
using Cookbook.Serializer.Recipe.Json;
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
            return await LoadAsync(new MeasureFilter(), new List<string>());
        }

        public async static Task<List<Measure>> LoadAsync(MeasureFilter filter)
        {
            return await LoadAsync(filter, new List<string>());
        }

        public async static Task<List<Measure>> LoadAsync(List<string> fields)
        {
            return await LoadAsync(new MeasureFilter(), fields);
        }

        public async static Task<List<Measure>> LoadAsync(MeasureFilter filter, List<string> fields)
        {
            var sb = new StringBuilder();
            sb.Append(_url);

            bool isFirstParam = true;
            isFirstParam = AppendMeasureIds(filter.IdsToLoad, isFirstParam, sb);
            isFirstParam = AppendFields(fields, isFirstParam, sb);

            var serializer = new MeasureJsonSerializer();
            serializer.SetFields(fields);

            var values = await ServiceClientHelper.GetGzipJsonAsync(sb.ToString(), serializer);

            return values;
        }

        private static bool AppendMeasureIds(List<Guid> measureIds, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                measureIds
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

        public async static Task<bool> AddAsync(List<Measure> measures)
        {
            return await ServiceClientHelper.PostGzipJsonAsync(_url, new MeasureJsonSerializer(), measures);
        }

        public async static Task<bool> UpdateAsync(List<Measure> measures)
        {
            return await ServiceClientHelper.PutGzipJsonAsync(_url, new MeasureJsonSerializer(), measures);
        }
    }
}
