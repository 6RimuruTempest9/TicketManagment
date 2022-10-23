using System.Threading.Tasks;

namespace TicketManagement.UserApi.BusinessLogic.Validation.Validators
{
    public interface IValidator<TDto>
    {
        public Task<ValidationResult> Validate(TDto dto);
    }
}