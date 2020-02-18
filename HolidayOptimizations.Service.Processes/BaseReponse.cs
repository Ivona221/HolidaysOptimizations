using HolidayOptimizations.Service.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HolidayOptimizations.Service.Processes
{
    public class BaseReponse<T>
    {
        public T Response { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public Exception Exception { get; set; }
    }
}
