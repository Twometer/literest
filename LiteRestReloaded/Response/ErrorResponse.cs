using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LiteRestReloaded.Response
{
    public class ErrorResponse : IResponse
    {
        private readonly HttpStatusCode statusCode;
        public ErrorResponse(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public string ContentType => "text/html";

        public int StatusCode => (int)statusCode;

        public string Write()
        {
            var codeDesc = Regex.Replace(statusCode.ToString(), "(\\B[A-Z])", " $1");
            return $"<center><h1>{StatusCode} {codeDesc}</h1></center>";
        }
    }
}
