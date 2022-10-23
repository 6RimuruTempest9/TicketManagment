using System;
using System.Security.Claims;
using TicketManagement.UserApi.BusinessLogic.Validation;

namespace TicketManagement.UserApi.BusinessLogic.Services.Results
{
    public class LoginResult : Result
    {
        public LoginResult(ValidationResult validationResult)
            : base(validationResult)
        {
        }

        public LoginResult(string jwt, ValidationResult validationResult)
            : base(validationResult)
        {
            Jwt = jwt ?? throw new ArgumentNullException(nameof(jwt));
        }

        public string Jwt { get; }
    }
}