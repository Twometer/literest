using LiteRestReloaded.Api;
using LiteRestReloaded.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace LiteRestReloaded.Handler
{
    internal class RequestHandler
    {
        private readonly ParsedController[] controllers;

        public RequestHandler(IController[] controllers)
        {
            this.controllers = new ParsedController[controllers.Length];
            for (var i = 0; i < controllers.Length; i++)
                this.controllers[i] = ParsedController.FromController(controllers[i]);
        }

        public void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var responseWriter = new ResponseWriter(context.Response);

            foreach (var controller in controllers)
            {
                if (!controller.Route.Equals(request.Url.AbsolutePath, StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var method in controller.Methods)
                {
                    if (method.Method != request.HttpMethod) continue;
                    if (method.Parameters.Length != request.QueryString.Count) continue;

                    var parameters = BuildParameters(request, method, out bool success);
                    if (!success)
                    {
                        responseWriter.WriteResponse(new ErrorResponse(HttpStatusCode.BadRequest));
                        return;
                    }

                    if (parameters.Length != method.Parameters.Length)
                        continue;

                    var result = method.Invoke(controller.Controller, parameters);
                    var json = JsonConvert.SerializeObject(result);


                    responseWriter.WriteResponse(new JsonResponse(json));
                    return;
                }
            }

            responseWriter.WriteResponse(new ErrorResponse(HttpStatusCode.NotFound));
        }

        private object[] BuildParameters(HttpListenerRequest request, ParsedMethod method, out bool success)
        {
            List<object> result = new List<object>();
            foreach (var param in method.Parameters)
            {
                if (param.Kind == ParameterKind.Query)
                {
                    foreach (var key in request.QueryString.AllKeys)
                    {
                        if (!param.Name.Equals(key, StringComparison.OrdinalIgnoreCase)) continue;
                        result.Add(ConvertParameter(request.QueryString.Get(key), param.Type, out var conversionSuccess));
                        if (!conversionSuccess)
                        {
                            success = false;
                            return null;
                        }
                    }
                }
            }
            success = true;
            return result.ToArray();
        }

        private object ConvertParameter(string input, Type targetType, out bool success)
        {
            try
            {
                success = true;
                if (targetType == typeof(byte))
                    return byte.Parse(input);
                else if (targetType == typeof(sbyte))
                    return sbyte.Parse(input);
                else if (targetType == typeof(short))
                    return short.Parse(input);
                else if (targetType == typeof(ushort))
                    return ushort.Parse(input);
                else if (targetType == typeof(int))
                    return int.Parse(input);
                else if (targetType == typeof(uint))
                    return uint.Parse(input);
                else if (targetType == typeof(long))
                    return long.Parse(input);
                else if (targetType == typeof(ulong))
                    return ulong.Parse(input);
                else if (targetType == typeof(char))
                    return char.Parse(input);
                else if (targetType == typeof(float))
                    return float.Parse(input);
                else if (targetType == typeof(double))
                    return double.Parse(input);
                else if (targetType == typeof(decimal))
                    return decimal.Parse(input);
                else if (targetType == typeof(bool))
                    return bool.Parse(input);
                else if (targetType == typeof(string))
                    return input;
                return input;
            }
            catch
            {
                success = false;
                return null;
            }
        }

    }
}
