using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Methods
{
    public abstract class HttpMethodAttribute : Attribute
    {
        internal string Method { get; }

        internal HttpMethodAttribute(string method)
        {
            Method = method;
        }
    }
}
