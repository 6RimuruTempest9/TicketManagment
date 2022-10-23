using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.CommonElements.Exceptions.Helpers;
using TicketManagement.DataAccess.Databases.EntityFrameworkCore;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Extensions;

namespace TicketManagement.DataAccess.Repositories.EntityFrameworkCore
{
    internal class EFRepository<TEntity> : IAsyncRepository<TEntity>
        where TEntity : Entity
    {
        private readonly EFDbContext _context;

        private readonly DbSet<TEntity> _models;

        private readonly ExceptionHelper _exceptionHelper;

        public EFRepository(EFDbContext context, ExceptionHelper exceptionHelper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _exceptionHelper = exceptionHelper ?? throw new ArgumentNullException(nameof(context));

            _models = context.Set<TEntity>();
        }

        private protected EFDbContext Context => _context;

        private protected ExceptionHelper ExceptionHelper => _exceptionHelper;

        public virtual Task DeleteAsync(int id)
        {
            return _exceptionHelper.ExecuteWithEFRepositoryExceptionAsync(async () =>
            {
                CheckIdMoreThanZeroElseThrownException(id);
                var model = await GetByIdAsync(id);
                _models.Remove(model);
                await Context.SaveChangesAsync();
            }, "An error occurred while deleting an model.");
        }

        public IQueryable<TEntity> GetAll()
        {
            return _exceptionHelper.ExecuteWithEFRepositoryException(() =>
                _models.AsQueryable(), "An error occurred while getting all models.");
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return _exceptionHelper.ExecuteWithEFRepositoryException(() =>
            {
                CheckIdMoreThanZeroElseThrownException(id);
                return _models.FirstAsync(m => m.Id == id);
            }, "An error occurred while getting model by id.");
        }

        public virtual Task InsertAsync(TEntity entity)
        {
            return _exceptionHelper.ExecuteWithEFRepositoryException(() =>
            {
                entity.IsNotNull();
                _models.Add(entity);
                return Context.SaveChangesAsync();
            }, "An error occurred while inserting model.");
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return _exceptionHelper.ExecuteWithEFRepositoryExceptionAsync(async () =>
            {
                CheckIdMoreThanZeroElseThrownException(entity.Id);
                entity.IsNotNull();
                var updatingModel = await _models.FirstAsync(m => m.Id == entity.Id);
                Context.Entry(updatingModel).CurrentValues.SetValues(entity);
                await Context.SaveChangesAsync();
            }, "An error occurred while updating model.");
        }

        private protected void CheckIdMoreThanZeroElseThrownException(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
        }
    }
}