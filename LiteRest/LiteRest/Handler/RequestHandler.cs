using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using LiteRest.Api;

namespace LiteRest.Handler
{
    public class RequestHandler
    {
        private ApiController[] controllers;

        public RequestHandler(ApiController[] controllers)
        {
            this.controllers = controllers;
        }

        public void HandleRequest(HttpListenerContext context)
        {
            ThreadPool.QueueUserWorkItem(c => { OnRequest(context); });
        }

        private void OnRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            /* SendReponse(response, "text/html", "<style>*{font-family:sans-serif;}</style><h1>Request info</h1>" +
                                               $"Method: {request.HttpMethod}<br>" +
                                               $"URL: {request.RawUrl}<br>" +
                                               $"Query: {request.Url.Query}<br>" +
                                               $"Query len: {request.QueryString.Count}"); */
        }

        private void SendReponse(HttpListenerResponse response, string contentType, string content)
        {
            var buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.ContentType = contentType;

            var outputStream = response.OutputStream;
            outputStream.Write(buffer, 0, buffer.Length);
            outputStream.Close();
        }
    }
}
