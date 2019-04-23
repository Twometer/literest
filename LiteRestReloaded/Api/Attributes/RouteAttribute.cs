using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes
{
    public class RouteAttribute : Attribute
    {
        public string Route { get; }

        public RouteAttribute(string route)
        {
            Route = route;
        }
    }
}
