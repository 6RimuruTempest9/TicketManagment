using System;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Extensions
{
    public static class EntityExtensions
    {
        public static Entity IsNotNull(this Entity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model;
        }
    }
}