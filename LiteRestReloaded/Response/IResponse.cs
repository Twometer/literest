using System;
using System.Collections.Generic;
using System.Text;

namespace LiteRestReloaded.Response
{
    public interface IResponse
    {
        string ContentType { get; }

        int StatusCode { get; }

        string Write();
    }
}
