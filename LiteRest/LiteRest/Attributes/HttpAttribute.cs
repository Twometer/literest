using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteRest.Attributes
{
    public abstract class HttpAttribute : Attribute
    {
        internal HttpAttribute(string method)
        {
            Method = method;
        }

        internal string Method { get; }

    }
}
