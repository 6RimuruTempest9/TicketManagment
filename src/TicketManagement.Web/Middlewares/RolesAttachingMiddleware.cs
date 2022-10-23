using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketManagement.Web.Extensions;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Results;

namespace TicketManagement.Web.Middlewares
{
    public class RolesAttachingMiddleware
    {
        private readonly RequestDelegate _next;

        public RolesAttachingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AuthorizationHttpClient authorizationHttpClient)
        {
            var jwt = context.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(jwt))
            {
                var requestRolesResult = await authorizationHttpClient.RequestRolesAsync(jwt);

                if (requestRolesResult.ResultType == ResultType.Failure)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    return;
                }

                context.Items["UserRoles"] = requestRolesResult.Roles;
            }

            await _next.Invoke(context);
        }
    }
}