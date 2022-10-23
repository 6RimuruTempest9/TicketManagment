using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketManagement.UserApi.BusinessLogic.Validation
{
    public class ValidationResult
    {
        private readonly List<ValidationError> _validationErrors = new List<ValidationError>();

        public IEnumerable<ValidationError> Errors => _validationErrors;

        public bool IsValid => !_validationErrors.Any();

        public int ErrorCount => _validationErrors.Count;

        public void AddError(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var validationError = new ValidationError(message);

            AddError(validationError);
        }

        public void AddError(string message, Exception exception)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var validationError = new ValidationError(message, exception);

            AddError(validationError);
        }

        public void AddError(ValidationError validationError)
        {
            if (validationError == null)
            {
                throw new ArgumentNullException(nameof(validationError));
            }

            _validationErrors.Add(validationError);
        }
    }
}