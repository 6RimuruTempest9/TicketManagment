using System;
using System.Runtime.Serialization;

namespace TicketManagement.CommonElements.Exceptions.Models
{
    [Serializable]
    public class ModelException : Exception
    {
        public ModelException(string message, string modelName)
            : base(message + " (" + modelName + ")")
        {
        }

        protected ModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}