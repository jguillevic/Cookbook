using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Tools.Service.Http
{
    public class HttpServer
    {
        private string _baseUrl;
        private Dictionary<string, Action<HttpListenerContext>> _actions;

        public HttpServer(string baseUrl, Dictionary<string, Action<HttpListenerContext>> actions)
        {
            _baseUrl = baseUrl;
            _actions = actions;
        }

        public void Start()
        {
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(_baseUrl);

                listener.Start();

                while (true)
                {
                    var context = listener.GetContext();

                    var thread = new Thread(() => Process(context));
                    thread.Start();
                }
            }
        }

        private void Process(HttpListenerContext context)
        {
            try
            {
                var route = context.Request.RawUrl.ToLower().Split('?')[0];

                if (_actions.ContainsKey(route))
                {
                    _actions[route](context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                }

                context.Response.OutputStream.Close();
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
