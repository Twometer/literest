using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LiteRestReloaded.Response
{
    public class ResponseWriter
    {
        private readonly HttpListenerResponse response;

        public ResponseWriter(HttpListenerResponse response)
        {
            this.response = response;
        }

        public void WriteResponse(IResponse response)
        {
            var buffer = Encoding.UTF8.GetBytes(response.Write());
            this.response.ContentLength64 = buffer.Length;
            this.response.ContentType = response.ContentType;
            this.response.StatusCode = response.StatusCode;

            var outputStream = this.response.OutputStream;
            outputStream.Write(buffer);
            outputStream.Close();
        }

    }
}
