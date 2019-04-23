using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Api.Attributes.Methods
{
    public class PostAttribute : HttpMethodAttribute
    {
        public PostAttribute() : base("POST")
        {
        }
    }
}
