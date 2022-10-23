using Microsoft.Extensions.DependencyInjection;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Extensions;

namespace TicketManagement.BusinessLogic.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddRepositories();

            services.AddScoped<IServiceAsync<Venue>, VenueService>();
            services.AddScoped<IServiceAsync<Layout>, LayoutService>();
            services.AddScoped<IServiceAsync<Area>, AreaService>();
            services.AddScoped<IServiceAsync<Seat>, SeatService>();
            services.AddScoped<IServiceAsync<Purchase>, PurchaseService>();
        }
    }
}