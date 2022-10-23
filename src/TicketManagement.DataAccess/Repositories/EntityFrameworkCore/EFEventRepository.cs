using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketManagement.CommonElements.Exceptions.Helpers;
using TicketManagement.DataAccess.Databases.EntityFrameworkCore;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Extensions;

namespace TicketManagement.DataAccess.Repositories.EntityFrameworkCore
{
    internal class EFEventRepository : EFRepository<Event>
    {
        public EFEventRepository(EFDbContext context, ExceptionHelper exceptionHelper)
            : base(context, exceptionHelper)
        {
        }

        public override Task DeleteAsync(int id)
        {
            return ExceptionHelper.ExecuteWithEFRepositoryException(() =>
            {
                CheckIdMoreThanZeroElseThrownException(id);
                return Context.Database.ExecuteSqlRawAsync("DeleteEvent @id", new SqlParameter("@id", id));
            }, "An error occurred while deleting an model.");
        }

        public override Task InsertAsync(Event entity)
        {
            return ExceptionHelper.ExecuteWithEFRepositoryException(() =>
            {
                entity.IsNotNull();
                var parameters = new[]
                {
                    new SqlParameter("@name", entity.Name),
                    new SqlParameter("@description", entity.Description),
                    new SqlParameter("@layoutId", entity.LayoutId),
                    new SqlParameter("@startDate", entity.StartDate),
                    new SqlParameter("@endDate", entity.EndDate),
                    new SqlParameter("@imageUrl", entity.ImageUrl),
                };
                return Context.Database.ExecuteSqlRawAsync("AddEvent @name, @description, @startDate, @endDate, @imageUrl, @layoutId", parameters);
            }, "An error occurred while inserting model.");
        }

        public override Task UpdateAsync(Event entity)
        {
            return ExceptionHelper.ExecuteWithEFRepositoryException(() =>
            {
                CheckIdMoreThanZeroElseThrownException(entity.Id);
                entity.IsNotNull();
                var parameters = new[]
                {
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@name", entity.Name),
                    new SqlParameter("@description", entity.Description),
                    new SqlParameter("@layoutId", entity.LayoutId),
                    new SqlParameter("@startDate", entity.StartDate),
                    new SqlParameter("@endDate", entity.EndDate),
                    new SqlParameter("@imageUrl", entity.ImageUrl),
                };
                return Context.Database.ExecuteSqlRawAsync("UpdateEvent @id, @name, @description, @layoutId, @startDate, @endDate, @imageUrl", parameters);
            }, "An error occurred while updating model.");
        }
    }
}