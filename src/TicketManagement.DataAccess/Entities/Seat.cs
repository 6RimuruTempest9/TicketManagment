using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Entities
{
    [Table("Seat")]
    public class Seat : Entity
    {
        public int AreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }
    }
}