using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using LiteRest.Api;
using LiteRest.Attributes;
using Newtonsoft.Json;

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

            foreach (var controller in controllers)
            {
                var type = controller.GetType();
                if (!(type.GetCustomAttributes(typeof(ControllerAttribute), true).FirstOrDefault() is ControllerAttribute controllerAttribute)) continue;
                if (!controllerAttribute.Route.Equals(request.Url.AbsolutePath, StringComparison.OrdinalIgnoreCase)) continue;
                foreach (var method in type.GetMethods())
                {
                    if (!(method.GetCustomAttributes(typeof(HttpAttribute), true).FirstOrDefault() is HttpAttribute httpAttribute)) continue;
                    if (httpAttribute.Method != request.HttpMethod) continue;

                    var parameters = new List<object>();
                    foreach (var param in method.GetParameters())
                    {
                        var paramCount = parameters.Count;
                        foreach (var key in request.QueryString.AllKeys)
                        {
                            if (!param.Name.Equals(key, StringComparison.OrdinalIgnoreCase)) continue;
                            parameters.Add(ConvertParameter(request.QueryString.Get(key), param, out var success));
                            if (!success)
                            {
                                SendReponse(response, "text/html", "<style>*{font-family:sans-serif;}</style>" +
                                                                   "<h1>Error 400 Bad Request</h1>" +
                                                                   "Received a malformed request");
                                return;
                            }
                            break;
                        }
                        if (parameters.Count == paramCount) parameters.Add(null);
                    }

                    var result = method.Invoke(controller, parameters.ToArray());
                    var responseMessage = JsonConvert.SerializeObject(result);

                    SendReponse(response, "application/json", responseMessage);
                    return;
                }
            }
            SendReponse(response, "text/html", "<style>*{font-family:sans-serif;}</style>" +
                                               "<h1>Error 404 Not Found</h1>" +
                                               "The requested URL could not be found on this server" +
                                               "<h3>Request info</h3>" +
                                               $"Method: {request.HttpMethod}<br>" +
                                               $"URL: {request.RawUrl}<br>" +
                                               $"Query: {request.Url.Query}<br>" +
                                               $"Query len: {request.QueryString.Count}");
        }

        private object ConvertParameter(string input, ParameterInfo info, out bool success)
        {
            try
            {
                success = true;
                if (info.ParameterType == typeof(int))
                    return Convert.ToInt32(input);
                if (info.ParameterType == typeof(long))
                    return Convert.ToInt64(input);
                if (info.ParameterType == typeof(short))
                    return Convert.ToInt16(input);
                return input;
            }
            catch
            {
                success = false;
                return null;
            }
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
