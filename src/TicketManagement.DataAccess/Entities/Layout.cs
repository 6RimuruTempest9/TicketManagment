using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("Layout")]
    public class Layout : Entity
    {
        public int VenueId { get; set; }

        public string Description { get; set; }
    }
}