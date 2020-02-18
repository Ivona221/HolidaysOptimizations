using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HolidayOptimizations.Web.Attributes
{
    public class ThrottleAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public int Milliseconds { get; set; }
        public string Message { get; set; }

        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext c)
        {
            var key = string.Concat(Name, "-", c.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);

            if (!Cache.TryGetValue(key, out bool entry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMilliseconds(Milliseconds));

                Cache.Set(key, true, cacheEntryOptions);
            }
            else
            {
                if (string.IsNullOrEmpty(Message))
                    Message = "You may only perform this action every {n} milliseconds.";

                c.Result = new ContentResult { Content = Message.Replace("{n}", Milliseconds.ToString()) };
                c.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }
    }
}
