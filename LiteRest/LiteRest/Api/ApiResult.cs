using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteRest.Api
{
    public class ApiResult<T>
    {

        public T Value { get; }

        public ApiResult(T value)
        {
            Value = value;
        }

    }
}
