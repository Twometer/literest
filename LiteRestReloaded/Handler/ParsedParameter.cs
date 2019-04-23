using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Handler
{
    internal class ParsedParameter
    {
        public string Name { get; }

        public Type Type { get; }

        public ParameterKind Kind { get; }

        public ParsedParameter(string name, Type type, ParameterKind kind)
        {
            Name = name;
            Type = type;
            Kind = kind;
        }
    }
}
