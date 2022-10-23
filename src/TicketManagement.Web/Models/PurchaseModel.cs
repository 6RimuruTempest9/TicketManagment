using System.Collections.Generic;

namespace TicketManagement.Web.Models
{
    public class PurchaseModel
    {
        public decimal TicketPrice { get; set; }

        public IEnumerable<int> EventSeatIds { get; set; }
    }
}