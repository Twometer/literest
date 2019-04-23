using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Response
{
    public class JsonResponse : IResponse
    {
        private readonly string json;

        public JsonResponse(string json)
        {
            this.json = json;
        }

        public string ContentType => "application/json";

        public int StatusCode => 200;

        public string Write()
        {
            return json;
        }
    }
}
