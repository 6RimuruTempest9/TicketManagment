using FluentValidation;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.BusinessLogic.Validators
{
    internal class AreaValidator : AbstractValidator<Area>
    {
        public AreaValidator()
        {
            RuleFor(area => area).NotNull();
            RuleFor(area => area.Id).GreaterThan(0);
            RuleFor(area => area.Description).NotEmpty();
        }
    }
}