using System;
using System.Security.Claims;
using TicketManagement.UserApi.BusinessLogic.Validation;

namespace TicketManagement.UserApi.BusinessLogic.Services.Results
{
    public class RegistrationResult : Result
    {
        public RegistrationResult(ValidationResult validationResult)
            : base(validationResult)
        {
        }

        public RegistrationResult(string jwt, ValidationResult validationResult)
            : base(validationResult)
        {
            Jwt = jwt ?? throw new ArgumentNullException(nameof(jwt));
        }

        public string Jwt { get; }
    }
}