using System.Threading.Tasks;

namespace TicketManagement.EventManagerApi.Services.Validation.Validators
{
    public interface IAsyncValidator<TModel>
    {
        public Task ValidateToCreateAsync(TModel model);

        public Task ValidateToUpdateAsync(TModel model);

        public Task ValidateToDeleteAsync(int id);
    }
}