using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("EventSeat")]
    public class EventSeat : Model
    {
        public int EventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public EventSeatState State { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}