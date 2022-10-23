namespace TicketManagement.Web.HttpClients.Options
{
    public class AuthorizationHttpClientOptions : HttpClientOptions
    {
        public string RegisterUrl { get; set; }

        public string LoginUrl { get; set; }

        public string RequestRolesUrl { get; set; }
    }
}