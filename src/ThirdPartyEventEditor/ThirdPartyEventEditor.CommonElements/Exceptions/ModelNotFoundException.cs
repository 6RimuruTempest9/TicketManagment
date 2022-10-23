using System;
using System.Runtime.Serialization;

namespace ThirdPartyEventEditor.CommonElements.Exceptions
{
    [Serializable]
    public class ModelNotFoundException<TModel> : Exception
    {
        public ModelNotFoundException()
            : base()
        {
        }

        public ModelNotFoundException(string message)
            : base(message)
        {
        }

        public ModelNotFoundException(string message, Exception innerException)
               : base(message, innerException)
        {
        }

        protected ModelNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}