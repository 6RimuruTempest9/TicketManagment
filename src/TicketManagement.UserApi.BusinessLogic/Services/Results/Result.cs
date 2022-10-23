using System;
using TicketManagement.UserApi.BusinessLogic.Validation;

namespace TicketManagement.UserApi.BusinessLogic.Services.Results
{
    public class Result
    {
        private readonly ValidationResult _validationResult;

        public Result(ValidationResult validationResult)
        {
            _validationResult = validationResult ?? throw new ArgumentNullException(nameof(validationResult));
        }

        public bool Succeeded => _validationResult.IsValid;

        public ValidationResult ValidationResult => _validationResult;
    }
}