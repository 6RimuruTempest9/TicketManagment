using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    public interface IAsyncRepository<TEntity>
        where TEntity : Entity
    {
        public IQueryable<TEntity> GetAll();

        public Task<TEntity> GetByIdAsync(int id);

        public Task InsertAsync(TEntity entity);

        public Task UpdateAsync(TEntity entity);

        public Task DeleteAsync(int id);
    }
}