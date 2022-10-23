using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using TicketManagement.Web.HttpClients;

namespace TicketManagement.Web.Middlewares
{
    public class LocalizeMiddleware
    {
        private static readonly Dictionary<string, string> _cultures = new Dictionary<string, string>
        {
            { "Russian", "ru-RU" },
            { "English", "en-US" },
            { "Belarussian", "be-BY" },
        };

        private readonly RequestDelegate _next;

        public LocalizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManagerHttpClient userManagerHttpClient)
        {
            var culture = new RequestCulture(_cultures["English"]);

            if (context.Session.Keys.Contains("JWT"))
            {
                var jwt = context.Session.GetString("JWT");

                var getUserByJwtResult = await userManagerHttpClient.GetUserByJwtAsync(jwt);

                if (getUserByJwtResult.ResultType == HttpClients.Results.ResultType.Success)
                {
                    var userModel = getUserByJwtResult.UserModel;

                    culture = new RequestCulture(_cultures[userModel.Language]);
                }
            }

            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(culture));

            await _next.Invoke(context);
        }
    }
}