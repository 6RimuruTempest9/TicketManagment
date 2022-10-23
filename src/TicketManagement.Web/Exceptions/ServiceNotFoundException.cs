using System;
using System.Runtime.Serialization;

namespace TicketManagement.Web.Exceptions
{
    [Serializable]
    public class ServiceNotFoundException<TServiceType> : Exception
    {
        public ServiceNotFoundException()
            : base("Service " + typeof(TServiceType).FullName + " was not found.")
        {
        }

        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}