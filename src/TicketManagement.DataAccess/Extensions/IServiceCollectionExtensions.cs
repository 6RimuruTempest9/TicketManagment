using Microsoft.Extensions.DependencyInjection;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Repositories;
using TicketManagement.DataAccess.Repositories.EntityFrameworkCore;

namespace TicketManagement.DataAccess.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAsyncRepository<Area>, EFAreaRepository>();
            services.AddScoped<IAsyncRepository<Seat>, EFSeatRepository>();
            services.AddScoped<IAsyncRepository<Venue>, EFVenueRepository>();
            services.AddScoped<IAsyncRepository<Layout>, EFLayoutRepository>();
            services.AddScoped<IAsyncRepository<Event>, EFEventRepository>();
            services.AddScoped<IAsyncRepository<EventArea>, EFEventAreaRepository>();
            services.AddScoped<IAsyncRepository<EventSeat>, EFEventSeatRepository>();
            services.AddScoped<IAsyncRepository<Purchase>, EFPurchaseRepository>();
        }
    }
}