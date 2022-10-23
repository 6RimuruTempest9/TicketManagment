using System;
using TicketManagement.Web.Exceptions;

namespace TicketManagement.Web.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static TServiceType GetService<TServiceType>(this IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof(TServiceType)) ?? throw new ServiceNotFoundException<TServiceType>();

            return (TServiceType)service;
        }
    }
}