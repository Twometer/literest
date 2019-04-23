using System;

namespace LiteRestReloaded.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var webApp = WebApp.Create(
                    new TestController()
                );
            webApp.Start("http://127.0.0.1:8080/");
        }
    }
}
