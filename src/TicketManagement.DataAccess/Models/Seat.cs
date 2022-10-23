using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("Seat")]
    public class Seat : Model
    {
        public int AreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}