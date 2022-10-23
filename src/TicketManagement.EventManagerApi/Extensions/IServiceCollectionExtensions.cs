using Microsoft.Extensions.DependencyInjection;
using TicketManagement.EventManagerApi.Models;
using TicketManagement.EventManagerApi.Services;
using TicketManagement.EventManagerApi.Services.Validation.Validators;

namespace TicketManagement.EventManagerApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAsyncService<Event>, EventService>();
            services.AddScoped<IAsyncService<EventArea>, EventAreaService>();
            services.AddScoped<IAsyncService<EventSeat>, EventSeatService>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IAsyncValidator<Event>, EventValidator>();
        }
    }
}