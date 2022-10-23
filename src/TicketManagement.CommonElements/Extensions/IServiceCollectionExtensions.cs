using Microsoft.Extensions.DependencyInjection;
using TicketManagement.CommonElements.Exceptions.Helpers;

namespace TicketManagement.CommonElements.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddExceptionHelper(this IServiceCollection services)
        {
            services.AddScoped<ExceptionHelper>();
        }
    }
}