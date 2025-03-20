using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class Response<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public T data { get; set; } = default(T);
    }
}
