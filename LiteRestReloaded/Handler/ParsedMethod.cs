using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LiteRestReloaded.Handler
{
    internal class ParsedMethod
    {
        private MethodInfo methodInfo;

        public string Method { get; }

        public ParsedParameter[] Parameters { get; }

        public ParsedMethod(MethodInfo methodInfo, string method, ParsedParameter[] parameters)
        {
            this.methodInfo = methodInfo;
            Method = method;
            Parameters = parameters;
        }

        public object Invoke(object target, object[] parameters)
        {
            return methodInfo.Invoke(target, parameters);
        }
    }
}
