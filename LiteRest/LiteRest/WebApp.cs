using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiteRest.Api;
using LiteRest.Handler;

namespace LiteRest
{
    public class WebApp
    {
        private readonly HttpListener httpListener;
        private readonly RequestHandler requestHandler;

        private WebApp(ApiController[] controllers)
        {
            httpListener = new HttpListener();
            requestHandler = new RequestHandler(controllers);
        }

        public static WebApp Create(params ApiController[] apiControllers)
        {
            return new WebApp(apiControllers);
        }

        public void Start(string mapping)
        {
            if (httpListener.IsListening) throw new InvalidOperationException("Cannot start webapp twice");
            httpListener.Prefixes.Add(mapping);
            httpListener.Start();
            while (httpListener.IsListening)
            {
                var context = httpListener.GetContext();
                requestHandler.HandleRequest(context);
            }
        }

        public void Stop()
        {
            httpListener.Stop();
        }
    }
}
