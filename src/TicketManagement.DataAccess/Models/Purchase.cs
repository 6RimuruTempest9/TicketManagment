using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("Purchase")]
    public class Purchase : Model
    {
        public string UserId { get; set; }

        public int EventSeatId { get; set; }

        public DateTime Time { get; set; }

        public decimal Price { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}