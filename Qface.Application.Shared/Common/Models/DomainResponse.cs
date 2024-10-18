using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Application.Shared.Common.Models
{
    public abstract class DomainResponseBase
    {
        public DomainResponseBase(string message, HttpStatusCode code)
        {
            Message = message;
            StatusCode = code;
        }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public int Code => ((int)StatusCode);

    }
}
