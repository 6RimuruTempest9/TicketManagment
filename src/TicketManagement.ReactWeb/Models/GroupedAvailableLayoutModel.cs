using System.Collections.Generic;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.ReactWeb.Models
{
    public class GroupedAvailableLayoutModel
    {
        public Venue Venue { get; set; }

        public IEnumerable<Layout> Layouts { get; set; }
    }
}