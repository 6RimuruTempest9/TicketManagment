using TicketManagement.CommonElements.Exceptions.Helpers;
using TicketManagement.DataAccess.Databases.EntityFrameworkCore;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Repositories.EntityFrameworkCore
{
    internal class EFLayoutRepository : EFRepository<Layout>
    {
        public EFLayoutRepository(EFDbContext context, ExceptionHelper exceptionHelper)
            : base(context, exceptionHelper)
        {
        }
    }
}