using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.DataAccess.Models
{
    [Table("Event")]
    public class Event : Model
    {
        public int LayoutId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ImageUrl { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}