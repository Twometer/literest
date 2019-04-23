using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Parameters
{
    public class QueryAttribute : ParameterAttribute
    {
        public QueryAttribute()
        {
        }

        public QueryAttribute(string name) : base(name)
        {
        }
    }
}
