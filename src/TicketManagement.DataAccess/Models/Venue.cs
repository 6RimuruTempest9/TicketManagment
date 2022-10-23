using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("Venue")]
    public class Venue : Model
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}