using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.DataAccess.Entities;
using TicketManagement.UserApi.BusinessLogic.Dto;

namespace TicketManagement.UserApi.BusinessLogic.Validation.Validators
{
    public class LoginDtoValidator : IValidator<LoginDto>
    {
        private readonly UserManager<User> _userManager;

        public LoginDtoValidator(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ValidationResult> Validate(LoginDto dto)
        {
            var validationResult = new ValidationResult();

            var userWithLoginEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (userWithLoginEmail == null)
            {
                validationResult.AddError("User with this email is not exist.");

                return validationResult;
            }

            var passwordVerificationResult = _userManager
                .PasswordHasher
                .VerifyHashedPassword(userWithLoginEmail, userWithLoginEmail.PasswordHash, dto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                validationResult.AddError("Incorrect password.");
            }

            return validationResult;
        }
    }
}