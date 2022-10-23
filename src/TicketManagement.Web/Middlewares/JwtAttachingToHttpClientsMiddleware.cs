using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketManagement.Web.Extensions;
using TicketManagement.Web.HttpClients;

namespace TicketManagement.Web.Middlewares
{
    public class JwtAttachingToHttpClientsMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAttachingToHttpClientsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwt = context.Session.GetString("JWT");

            if (!string.IsNullOrEmpty(jwt))
            {
                var headerName = "Authorization";
                var headerValue = "Bearer " + jwt;

                var httpClients = GetHttpClients(context);

                foreach (var httpClient in httpClients)
                {
                    httpClient.SetHeader(headerName, headerValue);
                }
            }

            await _next.Invoke(context);
        }

        private IEnumerable<HttpClientBase> GetHttpClients(HttpContext context)
        {
            var httpClients = new List<HttpClientBase>();

            httpClients.Add(context.RequestServices.GetService<AuthorizationHttpClient>());
            httpClients.Add(context.RequestServices.GetService<EventManagerHttpClient>());
            httpClients.Add(context.RequestServices.GetService<UserManagerHttpClient>());

            return httpClients;
        }
    }
}