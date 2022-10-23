using System;

namespace TicketManagement.UserApi.BusinessLogic.Validation
{
    public class ValidationError
    {
        private readonly string _message;

        public ValidationError(string message)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public ValidationError(string message, Exception exception)
            : this(message)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public string Message => _message;

        public Exception Exception { get; }
    }
}