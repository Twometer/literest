using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Parameters
{
    public class PathAttribute : ParameterAttribute
    {
        public PathAttribute()
        {
        }

        public PathAttribute(string name) : base(name)
        {
        }
    }
}
