namespace TicketManagement.Web.HttpClients.Options
{
    public class UserManagerHttpClientOptions : HttpClientOptions
    {
        public string GetUserByJwtUrl { get; set; }

        public string UpdateUserUrl { get; set; }

        public string ChangePasswordUrl { get; set; }
    }
}