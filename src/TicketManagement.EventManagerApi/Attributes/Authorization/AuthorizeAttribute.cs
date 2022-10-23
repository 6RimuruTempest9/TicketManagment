using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketManagement.EventManagerApi.HttpClients;

namespace TicketManagement.EventManagerApi.Attributes.Authorization
{
    public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string Roles { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var jwtValidationHttpClient
                = (JwtValidationHttpClient)context.HttpContext.RequestServices.GetService(typeof(JwtValidationHttpClient));

            var jwt = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (jwt == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                return;
            }

            var isJwtValid = await jwtValidationHttpClient.IsJwtValid(jwt);

            if (!isJwtValid)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                return;
            }

            var accessedRoles = await jwtValidationHttpClient.GetUserRolesByJwt(jwt);

            var roles = Roles.Split(",").Select(role => role.Trim());

            var isAccessAllowed = accessedRoles.Any(accessedRole => roles.Any(role => role == accessedRole));

            if (!isAccessAllowed)
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}