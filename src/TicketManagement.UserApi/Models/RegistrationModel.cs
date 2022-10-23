namespace TicketManagement.UserApi.Models
{
    public class RegistrationModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }
    }
}