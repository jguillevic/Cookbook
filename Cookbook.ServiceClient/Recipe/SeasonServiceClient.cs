﻿using Cookbook.Entity.Recipe;
using Cookbook.Serializer.Recipe.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
            return await LoadAsync(new List<string>());
        }

        public async static Task<List<Season>> LoadAsync(List<string> fields)
        {
            var sb = new StringBuilder();
            sb.Append(_url);

            bool isFirstParam = true;
            isFirstParam = AppendFields(fields, isFirstParam, sb);

            var serializer = new SeasonJsonSerializer();
            serializer.SetFields(fields);

            var values = await ServiceClientHelper.GetGzipJsonAsync(sb.ToString(), serializer);

            return values;
        }

        private static bool AppendFields(List<string> fields, bool isFirstParam, StringBuilder sb)
        {
            return ServiceClientHelper.AppendQueries(
                fields
                , item => { sb.Append(string.Format(CultureInfo.CurrentCulture, "field={0}", item)); }
                , isFirstParam
                , sb);
        }
    }
}
