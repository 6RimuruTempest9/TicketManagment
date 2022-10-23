using Microsoft.AspNetCore.Identity;

namespace TicketManagement.DataAccess.Models
{
    public class User : IdentityUser, IModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }

        public decimal Balance { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}