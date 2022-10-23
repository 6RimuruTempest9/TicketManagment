using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketManagement.Web.Attributes
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAccessAllowed = false;

            var isSessionContainsJwt = context.HttpContext.Request.Cookies.ContainsKey("JWT");

            if (isSessionContainsJwt)
            {
                isAccessAllowed = true;
            }

            if (isSessionContainsJwt && !string.IsNullOrEmpty(Roles))
            {
                if (!context.HttpContext.Items.ContainsKey("UserRoles"))
                {
                    isAccessAllowed = false;
                }
                else
                {
                    var userRoles = (IEnumerable<string>)context.HttpContext.Items["UserRoles"];

                    var roles = Roles.Split(",").Select(role => role.Trim());

                    isAccessAllowed = userRoles.Any(userRole => roles.Any(role => role == userRole));
                }
            }

            if (!isAccessAllowed)
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}