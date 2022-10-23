using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("Area")]
    public class Area : Model
    {
        public int LayoutId { get; set; }

        public string Description { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}