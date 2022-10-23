using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketManagement.Web.Attributes
{
    public class NonAuthorizeOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAccessAllowed = true;

            var isSessionContainsJwt = context.HttpContext.Session.Keys.Contains("JWT");

            if (isSessionContainsJwt)
            {
                isAccessAllowed = false;
            }

            if (!isAccessAllowed)
            {
                context.Result = new JsonResult(new { message = "Need logout before login/register." }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}