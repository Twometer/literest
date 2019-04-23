using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Parameters
{
    public abstract class ParameterAttribute : Attribute
    {
        public string Name { get; }

        internal ParameterAttribute(string name)
        {
            Name = name;
        }

        internal ParameterAttribute()
        {
        }
    }
}
