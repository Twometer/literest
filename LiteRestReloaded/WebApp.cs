using LiteRestReloaded.Api;
using LiteRestReloaded.Handler;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace LiteRestReloaded
{
    public class WebApp
    {
        private readonly HttpListener httpListener;

        private readonly RequestHandler requestHandler;

        private WebApp(HttpListener httpListener, RequestHandler requestHandler)
        {
            this.httpListener = httpListener;
            this.requestHandler = requestHandler;
        }

        public static WebApp Create(params IController[] controllers)
        {
            var listener = new HttpListener();
            var handler = new RequestHandler(controllers);
            return new WebApp(listener, handler);
        }

        public void Start(string mapping)
        {
            if (httpListener.IsListening)
                throw new InvalidOperationException("WebApp is already running");

            httpListener.Prefixes.Add(mapping);
            httpListener.Start();

            while (httpListener.IsListening)
            {
                var context = httpListener.GetContext();
                ThreadPool.QueueUserWorkItem(c =>
                {
                    requestHandler.HandleRequest(context);
                });
            }
        }

        public void Stop()
        {
            httpListener.Stop();
        }

    }
}
