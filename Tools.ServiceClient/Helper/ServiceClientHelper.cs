using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tools.Helper.Compress;
using Tools.Helper.Json;

namespace Tools.ServiceClient.Helper
{
    public static class ServiceClientHelper
    {
        public async static Task<T> GetGzipJsonAsync<T>(string url)
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
                                return JsonHelper.DeserializeFromStream<T>(gzip);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public async static Task<bool> PostGzipJsonAsync<T>(string url, T value)
        {
            using (var httpClient = new HttpClient())
            {
                using (var jsonStream = JsonHelper.SerializeToStream(value))
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

        public async static Task<bool> PutGzipJsonAsync<T>(string url, T value)
        {
            using (var httpClient = new HttpClient())
            {
                using (var jsonStream = JsonHelper.SerializeToStream(value))
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
    }
}
