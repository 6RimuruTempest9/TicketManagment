using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("Purchase")]
    public class Purchase : Entity
    {
        public string UserId { get; set; }

        public int EventSeatId { get; set; }

        public DateTime Time { get; set; }

        public decimal Price { get; set; }
    }
}