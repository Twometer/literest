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
        public ApiResult<string> GetCommands([UrlParameter] int limit)
        {
            return new ApiResult<string>("Test, limit was " + limit);
        }

        [HttpPost]
        public ApiResult<string> SendCommand([ContentParameter] string command)
        {
            return new ApiResult<string>("Test");
        }
    }
}
