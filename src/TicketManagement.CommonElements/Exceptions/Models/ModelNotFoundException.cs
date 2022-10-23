using System;
using System.Runtime.Serialization;

namespace TicketManagement.CommonElements.Exceptions.Models
{
    [Serializable]
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException(string modelName)
            : base(modelName + " not found.")
        {
        }

        public ModelNotFoundException(string message, string modelName)
            : base(message + " (" + modelName + ")")
        {
        }

        protected ModelNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}