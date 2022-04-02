using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Qalam.BLL.Managers;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using System;
using System.Linq;
using System.Net;

namespace Qalam.Configurations
{
    public class Authorization : ActionFilterAttribute
    {
        private EUserTypes[] Types { get; set; }
        public Authorization(params EUserTypes[] types) : base()
        {
            Types = types;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestAttribute = context.HttpContext.RequestServices.GetService<RequestAttributes>();
            var tokenManager = context.HttpContext.RequestServices.GetService<TokenManager>();

            requestAttribute.ResetValues();

            var httpContext = context.HttpContext;
            string authToken = httpContext.Request.Headers[HeaderNames.Authorization];

            try
            {
                if (string.IsNullOrEmpty(authToken))
                {
                    throw new Exception();
                }

                string token = authToken.Substring("Bearer ".Length).Trim();
                requestAttribute.CopyFrom(tokenManager.ValidateToken(token, Types));
            }
            catch (Exception)
            {
                if (Types.Any(r => r == EUserTypes.Guest))
                {
                    requestAttribute.ResetValues();
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
