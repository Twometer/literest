using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteRest.Testing.Controllers;

namespace LiteRest.Testing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var webApp = WebApp.Create(
                new TestController(),
                new BookshelfController()
                );
            webApp.Start("http://127.0.0.1:8080/");
        }
    }
}
