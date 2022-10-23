using Microsoft.AspNetCore.Identity;

namespace TicketManagement.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }

        public decimal Balance { get; set; }
    }
}