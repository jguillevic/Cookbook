using System.Linq;
using System.Net;

namespace Tools.Service.Http
{
    public static class HttpHelper
    {
        public static bool IsAcceptGZipJson(this HttpListenerContext context)
        {
            var headers = context.Request.Headers;

            if (headers.AllKeys.Contains("Accept")
                && headers.AllKeys.Contains("Accept-Encoding"))
            {
                var acceptType = headers.GetValues("Accept");
                var acceptEncodType = headers.GetValues("Accept-Encoding");

                if (acceptType.Contains("application/json")
                    && acceptEncodType.Contains("gzip"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsContentGZipJson(this HttpListenerContext context)
        {
            var headers = context.Request.Headers;

            if (headers.AllKeys.Contains("Content-Type")
                && headers.AllKeys.Contains("Content-Encoding"))
            {
                var contentType = headers.GetValues("Content-Type");
                var contentEncodType = headers.GetValues("Content-Encoding");

                if (contentType.Contains("application/json")
                    && contentEncodType.Contains("gzip"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
