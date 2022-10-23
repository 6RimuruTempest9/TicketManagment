namespace TicketManagement.Web.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }

        public decimal Balance { get; set; }
    }
}