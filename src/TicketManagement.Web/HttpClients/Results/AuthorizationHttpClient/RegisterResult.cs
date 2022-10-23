namespace TicketManagement.Web.HttpClients.Results.AuthorizationHttpClient
{
    public class RegisterResult : Result
    {
        public RegisterResult(ResultType resultType)
            : base(resultType)
        {
        }

        public string Jwt { get; set; }
    }
}