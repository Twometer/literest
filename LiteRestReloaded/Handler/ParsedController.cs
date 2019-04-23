using LiteRestReloaded.Api;
using LiteRestReloaded.Api.Attributes;
using LiteRestReloaded.Api.Attributes.Methods;
using LiteRestReloaded.Api.Attributes.Parameters;
using LiteRestReloaded.Util;
using System.Collections.Generic;

namespace LiteRestReloaded.Handler
{
    internal class ParsedController
    {
        public IController Controller { get; }

        public string Route { get; }

        public List<ParsedMethod> Methods { get; } = new List<ParsedMethod>();

        private ParsedController(IController controller, string route)
        {
            Controller = controller;
            Route = route;
        }

        public static ParsedController FromController(IController controller)
        {
            var type = controller.GetType();
            var routeAttribute = type.GetAttributeObject<RouteAttribute>();
            if (routeAttribute == null) return null;

            var result = new ParsedController(controller, routeAttribute.Route);

            foreach(var method in type.GetMethods())
            {
                var methodAttribute = method.GetAttributeObject<HttpMethodAttribute>();
                if (methodAttribute == null) continue;

                List<ParsedParameter> parameters = new List<ParsedParameter>();

                foreach(var param in method.GetParameters())
                {
                    var isQueryParameter = param.HasAttribute<QueryAttribute>();
                    var isBodyParameter = param.HasAttribute<BodyAttribute>();
                    var isPathParameter = param.HasAttribute<PathAttribute>();

                    ParameterKind kind;
                    if (isQueryParameter)
                        kind = ParameterKind.Query;
                    else if (isBodyParameter)
                        kind = ParameterKind.Body;
                    else if (isPathParameter)
                        kind = ParameterKind.Path;
                    else continue;

                    var parameterAttribute = param.GetAttributeObject<ParameterAttribute>();
                    if (parameterAttribute == null) continue;

                    var name = string.IsNullOrEmpty(parameterAttribute.Name) ? param.Name : parameterAttribute.Name;
                    parameters.Add(new ParsedParameter(name, param.ParameterType, kind));
                }

                result.Methods.Add(new ParsedMethod(method, methodAttribute.Method, parameters.ToArray()));
            }

            return result;
        }


    }
}
