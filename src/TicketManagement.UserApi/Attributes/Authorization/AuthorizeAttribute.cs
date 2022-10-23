using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketManagement.UserApi.BusinessLogic.Services;

namespace TicketManagement.UserApi.Attributes.Authorization
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwt = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (jwt == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                return;
            }

            var jwtTokenManager = (JwtTokenManager)context.HttpContext.RequestServices.GetService(typeof(JwtTokenManager));

            var isJwtValid = jwtTokenManager.IsValidToken(jwt);

            if (!isJwtValid)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                return;
            }

            var isAccessAllowed = true;

            if (!string.IsNullOrEmpty(Roles))
            {
                var userRoles = jwtTokenManager.GetUserRolesByJwt(jwt);

                var roles = Roles.Split(",").Select(role => role.Trim());

                isAccessAllowed = userRoles.Any(userRole => roles.Any(role => role == userRole));
            }

            if (!isAccessAllowed)
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}