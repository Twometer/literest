using LiteRestReloaded.Api;
using LiteRestReloaded.Api.Attributes;
using LiteRestReloaded.Api.Attributes.Methods;
using LiteRestReloaded.Api.Attributes.Parameters;

namespace LiteRestReloaded.Testing
{

    [Route("/test")]
    public class TestController : IController
    {

        [Get]
        public string Get([Query] ushort test)
        {
            return "Your test value was: " + test;
        }

        [Get]
        public string Test([Query("test_val")] ushort testVal)
        {
            return "Your second test val was: " + testVal;
        }

        [Get]
        public string Get()
        {
            return "Try using a test parameter";
        }

    }
}
