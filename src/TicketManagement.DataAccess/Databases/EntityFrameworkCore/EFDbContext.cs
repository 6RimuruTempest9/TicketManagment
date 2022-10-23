using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Databases.EntityFrameworkCore
{
    public class EFDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Layout> Layouts { get; set; }

        public DbSet<EventArea> EventAreas { get; set; }

        public DbSet<EventSeat> EventSeats { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
    }
}