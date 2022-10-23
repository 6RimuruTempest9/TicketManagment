namespace TicketManagement.Web.HttpClients.Results.AuthorizationHttpClient
{
    public class LoginResult : Result
    {
        public LoginResult(ResultType resultType)
            : base(resultType)
        {
        }

        public string Jwt { get; set; }
    }
}