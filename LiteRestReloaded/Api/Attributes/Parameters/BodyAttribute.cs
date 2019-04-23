using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Parameters
{
    public class BodyAttribute : ParameterAttribute
    {
        public BodyAttribute()
        {
        }

        public BodyAttribute(string name) : base(name)
        {
        }
    }
}
