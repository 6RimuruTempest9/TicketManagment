using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("EventArea")]
    public class EventArea : Model
    {
        public int EventId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public decimal Price { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}