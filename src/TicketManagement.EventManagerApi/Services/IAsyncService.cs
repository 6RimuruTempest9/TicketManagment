using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManagement.EventManagerApi.Services
{
    public interface IAsyncService<TModel>
    {
        public Task CreateAsync(TModel model);

        public Task UpdateAsync(TModel model);

        public Task DeleteAsync(int id);

        public Task<TModel> GetByIdAsync(int id);

        public Task<IEnumerable<TModel>> GetAllAsync();
    }
}