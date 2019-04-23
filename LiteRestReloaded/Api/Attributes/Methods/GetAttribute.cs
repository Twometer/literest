using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Methods
{
    public class GetAttribute : HttpMethodAttribute
    {
        public GetAttribute() : base("GET")
        {
        }
    }
}
