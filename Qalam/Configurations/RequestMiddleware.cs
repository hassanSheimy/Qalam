using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Qalam.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qalam.Configurations
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private RequestAttributes _requestAttributes;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, RequestAttributes requestAttributes)
        {
            
            _requestAttributes = requestAttributes;
            BeforExecution(httpContext);
            await _next(httpContext);
            AfterExecution(httpContext);
        }
        private void BeforExecution(HttpContext httpContext)
        {
            RequestAttributes.AppBaseUrl = String.Format("{0}://{1}", httpContext.Request.IsHttps ? "https" : "http", httpContext.Request.Host.Value);
        }

        private void AfterExecution(HttpContext httpContext)
        {
        }
    }
}
