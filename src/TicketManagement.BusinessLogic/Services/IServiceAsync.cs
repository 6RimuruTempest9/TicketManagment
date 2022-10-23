using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.Services
{
    public interface IServiceAsync<TModel>
        where TModel : Entity
    {
        public IQueryable<TModel> GetAll();

        public Task<TModel> GetByIdAsync(int id);

        public Task InsertAsync(TModel model);

        public Task UpdateAsync(int id, TModel model);

        public Task DeleteAsync(int id);
    }
}