using Microsoft.AspNetCore.Identity;

namespace TicketManagement.UserApi.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }

        public string Balance { get; set; }
    }
}