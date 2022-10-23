using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("Layout")]
    public class Layout : Model
    {
        public int VenueId { get; set; }

        public string Description { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}