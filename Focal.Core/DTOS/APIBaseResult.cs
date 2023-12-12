using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Core.DTOS
{
    public class APIBaseResult<T> where T : class
    {
        public List<string> Messages { get; set; }
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }

        public APIBaseResult()
        {
            Status = HttpStatusCode.InternalServerError;
            Messages = new List<string> { "Something wrong happened please contact admin" };
        }
        public APIBaseResult(HttpStatusCode status)
        {
            Status = status;
        }
        public APIBaseResult(List<string> messages, HttpStatusCode status)
        {
            Messages = messages;
            Status = status;
        }
    }
}
