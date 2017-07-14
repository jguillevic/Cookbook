using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tools.Helper.Compress;
using Tools.Serializer.Json;

namespace Tools.ServiceClient.Helper
{
    public static class ServiceClientHelper
    {
        public async static Task<T> GetGzipJsonAsync<T>(string url, IJsonSerializer<T> serializer)
        {
            T result = default(T);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                using (var httpResponse = await httpClient.GetAsync(url))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        using (var stream = await httpResponse.Content.ReadAsStreamAsync())
                        {
                            using (var gzip = GZipHelper.Decompress(stream))
                            {
                                return serializer.Deserialize(gzip);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public async static Task<bool> PostGzipJsonAsync<T>(string url, IJsonSerializer<T> serializer, T value)
        {
            using (var httpClient = new HttpClient())
            {
                using (var jsonStream = serializer.Serialize(value))
                {
                    using (var compStream = GZipHelper.Compress(jsonStream))
                    {
                        using (var httpContent = new StreamContent(compStream))
                        {
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            httpContent.Headers.ContentEncoding.Add("gzip");

                            using (var httpResponse = await httpClient.PostAsync(url, httpContent))
                            {
                                return httpResponse.IsSuccessStatusCode;
                            }
                        }
                    }
                }
            }
        }

        public async static Task<bool> PutGzipJsonAsync<T>(string url, IJsonSerializer<T> serializer, T value)
        {
            using (var httpClient = new HttpClient())
            {
                using (var jsonStream = serializer.Serialize(value))
                {
                    using (var compStream = GZipHelper.Compress(jsonStream))
                    {
                        using (var httpContent = new StreamContent(compStream))
                        {
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            httpContent.Headers.ContentEncoding.Add("gzip");

                            using (var httpResponse = await httpClient.PutAsync(url, httpContent))
                            {
                                return httpResponse.IsSuccessStatusCode;
                            }
                        }
                    }
                }
            }
        }

        public static bool AppendQueries<T>(List<T> values, Action<T> action, bool isFirstParam, StringBuilder sb)
        {
            if (values.Count > 0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (i == 0 && isFirstParam)
                        sb.Append("?");
                    else
                        sb.Append("&");

                    action(values[i]);
                }

                return false;
            }

            return isFirstParam;
        }

        public static bool AppendQueries<T>(Nullable<T> value, Action<Nullable<T>> action, bool isFirstParam, StringBuilder sb)
            where T : struct
        {
            if (value.HasValue)
            {
                if (isFirstParam)
                    sb.Append("?");
                else
                    sb.Append("&");

                action(value);

                return false;
            }

            return isFirstParam;
        }

        public static bool AppendQueries(string value, Action<string> action, bool isFirstParam, StringBuilder sb)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (isFirstParam)
                    sb.Append("?");
                else
                    sb.Append("&");

                action(value);

                return false;
            }

            return isFirstParam;
        }
    }
}
