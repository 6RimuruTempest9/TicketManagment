using System;
using System.Runtime.Serialization;

namespace ThirdPartyEventEditor.CommonElements.Exceptions
{
    [Serializable]
    public class ConvertException : Exception
    {
        public ConvertException(string message)
            : base(message)
        {
        }

        public ConvertException(string message, Exception innerException)
               : base(message, innerException)
        {
        }

        protected ConvertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}