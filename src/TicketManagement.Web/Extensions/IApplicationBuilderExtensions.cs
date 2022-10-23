using Microsoft.AspNetCore.Builder;
using TicketManagement.Web.Middlewares;

namespace TicketManagement.Web.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseJwtAttachingToHttpClients(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtAttachingToHttpClientsMiddleware>();
        }

        public static void UseRolesAttaching(this IApplicationBuilder app)
        {
            app.UseMiddleware<RolesAttachingMiddleware>();
        }
    }
}