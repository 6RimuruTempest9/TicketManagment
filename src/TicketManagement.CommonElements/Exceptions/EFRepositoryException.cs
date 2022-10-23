using System;
using System.Runtime.Serialization;

namespace TicketManagement.CommonElements.Exceptions
{
    [Serializable]
    public class EFRepositoryException : Exception
    {
        public EFRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EFRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}