using System.Collections.Generic;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic.DataTransferObjects
{
    public class GroupedAvailableLayoutModelDto
    {
        public Venue Venue { get; set; }

        public IEnumerable<Layout> Layouts { get; set; }
    }
}