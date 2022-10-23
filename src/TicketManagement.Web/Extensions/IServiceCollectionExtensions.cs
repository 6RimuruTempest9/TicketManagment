using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.BusinessLogic.Extensions;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Options;

namespace TicketManagement.Web.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddHttpClientOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthorizationHttpClientOptions>(configuration.GetSection("AuthorizationHttpClientOptions"));
            services.Configure<EventManagerHttpClientOptions>(configuration.GetSection("EventManagerHttpClientOptions"));
            services.Configure<UserManagerHttpClientOptions>(configuration.GetSection("UserManagerHttpClientOptions"));
        }

        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddScoped<AuthorizationHttpClient>();
            services.AddScoped<EventManagerHttpClient>();
            services.AddScoped<UserManagerHttpClient>();
        }
    }
}