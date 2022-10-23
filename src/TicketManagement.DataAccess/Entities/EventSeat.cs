using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("EventSeat")]
    public class EventSeat : Entity
    {
        public int EventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public EventSeatState State { get; set; }
    }
}