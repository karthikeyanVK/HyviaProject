using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Cads.VelVenti.Api.Filters
{
    public class CacheClientAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            if (actionExecutedContext.Response == null) return;
            if (Duration == 0) { Duration = 1000000000; }

            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(Duration),
                MustRevalidate = true,
                Public = true
            };

        }
    }
}