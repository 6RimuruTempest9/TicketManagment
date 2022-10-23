using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketManagement.CommonElements.Exceptions;
using TicketManagement.DataAccess.Entities;
using TicketManagement.UserApi.BusinessLogic.Dto;
using TicketManagement.UserApi.BusinessLogic.Services.Results;
using TicketManagement.UserApi.BusinessLogic.Validation;
using TicketManagement.UserApi.BusinessLogic.Validation.Validators;

namespace TicketManagement.UserApi.BusinessLogic.Services
{
    public class UserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenManager _jwtTokenManager;
        private readonly IValidator<RegistrationDto> _registrationDtoValidator;
        private readonly IValidator<LoginDto> _loginDtoValidator;

        public UserManager(UserManager<User> userManager,
            JwtTokenManager jwtTokenManager,
            IValidator<RegistrationDto> registrationDtoValidator,
            IValidator<LoginDto> loginDtoValidator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtTokenManager = jwtTokenManager ?? throw new ArgumentNullException(nameof(jwtTokenManager));
            _registrationDtoValidator = registrationDtoValidator ?? throw new ArgumentNullException(nameof(registrationDtoValidator));
            _loginDtoValidator = loginDtoValidator ?? throw new ArgumentNullException(nameof(loginDtoValidator));
        }

        public UserManager<User> IdentityUserManager => _userManager;

        public async Task<RegistrationResult> Register(RegistrationDto registrationDto)
        {
            var validationResult = await _registrationDtoValidator.Validate(registrationDto);

            if (validationResult.IsValid)
            {
                var user = new User
                {
                    UserName = registrationDto.Email,
                    Email = registrationDto.Email,
                    FirstName = registrationDto.FirstName,
                    LastName = registrationDto.LastName,
                    Language = registrationDto.Language,
                    TimeZone = registrationDto.TimeZone,
                };

                var result = await _userManager.CreateAsync(user, registrationDto.Password);

                foreach (var error in result.Errors)
                {
                    validationResult.AddError(error.Description);
                }
            }

            if (validationResult.ErrorCount == 0)
            {
                var user = await _userManager.FindByEmailAsync(registrationDto.Email);

                var roles = await _userManager.GetRolesAsync(user);

                var jwt = _jwtTokenManager.GetToken(user, roles);

                return new RegistrationResult(jwt, validationResult);
            }

            return new RegistrationResult(validationResult);
        }

        public async Task<LoginResult> Login(LoginDto loginDto)
        {
            var validationResult = await _loginDtoValidator.Validate(loginDto);

            if (validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                var roles = await _userManager.GetRolesAsync(user);

                var jwt = _jwtTokenManager.GetToken(user, roles);

                return new LoginResult(jwt, validationResult);
            }

            return new LoginResult(validationResult);
        }

        public async Task<ChangePasswordResult> ChangePasswordUserByIdAsync(string userId, string oldPassword, string newPassword)
        {
            var validationResult = new ValidationResult();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                validationResult.AddError("User not found.");

                return new ChangePasswordResult(validationResult);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            foreach (var error in changePasswordResult.Errors)
            {
                validationResult.AddError(error.Description);
            }

            return new ChangePasswordResult(validationResult);
        }

        public async Task<string> GetIdByJwt(string jwt)
        {
            var username = _jwtTokenManager.GetUsernameByJwt(jwt);

            var user = await _userManager.FindByNameAsync(username);

            return user.Id;
        }

        public bool IsJwtValid(string jwt)
        {
            var isJwtValid = _jwtTokenManager.IsValidToken(jwt);

            return isJwtValid;
        }

        public IEnumerable<string> GetUserRolesByJwt(string jwt)
        {
            var roles = _jwtTokenManager.GetUserRolesByJwt(jwt);

            return roles;
        }
    }
}