namespace TicketManagement.UserApi.BusinessLogic.Dto
{
    public class RegistrationDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string TimeZone { get; set; }
    }
}