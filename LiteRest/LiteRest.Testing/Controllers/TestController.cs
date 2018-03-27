using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteRest.Api;
using LiteRest.Attributes;

namespace LiteRest.Testing.Controllers
{
    [Controller("/api/test")]
    public class TestController : ApiController
    {

        [HttpGet]
        public void GetCommands([UrlParameter] int limit)
        {
            
        }

        [HttpPost]
        public void SendCommand([ContentParameter] string command)
        {
            
        }
    }
}
