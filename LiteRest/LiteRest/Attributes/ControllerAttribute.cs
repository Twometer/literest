using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteRest.Attributes
{
    public class ControllerAttribute : Attribute
    {
        public ControllerAttribute(string route)
        {
            Route = route;
        }

        public string Route { get; set; }
        
    }
}
