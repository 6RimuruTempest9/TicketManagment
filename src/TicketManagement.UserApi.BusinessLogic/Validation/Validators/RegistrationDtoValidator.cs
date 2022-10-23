using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.DataAccess.Entities;
using TicketManagement.UserApi.BusinessLogic.Dto;

namespace TicketManagement.UserApi.BusinessLogic.Validation.Validators
{
    public class RegistrationDtoValidator : IValidator<RegistrationDto>
    {
        private readonly UserManager<User> _userManager;

        public RegistrationDtoValidator(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ValidationResult> Validate(RegistrationDto dto)
        {
            var validationResult = new ValidationResult();

            var userWithRegistrationEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (userWithRegistrationEmail != null)
            {
                validationResult.AddError("User with this email is already exist.");
            }

            return validationResult;
        }
    }
}