using System;
using System.Runtime.Serialization;

namespace ThirdPartyEventEditor.CommonElements.Exceptions
{
    [Serializable]
    public class JsonRepositoryException : Exception
    {
        public JsonRepositoryException(string message)
            : base(message)
        {
        }

        public JsonRepositoryException(string message, Exception innerException)
               : base(message, innerException)
        {
        }

        protected JsonRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}