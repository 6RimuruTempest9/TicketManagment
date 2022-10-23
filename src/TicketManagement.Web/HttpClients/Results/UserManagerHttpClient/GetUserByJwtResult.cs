using TicketManagement.Web.Models;

namespace TicketManagement.Web.HttpClients.Results.UserManagerHttpClient
{
    public class GetUserByJwtResult : Result
    {
        public GetUserByJwtResult(ResultType resultType)
            : base(resultType)
        {
        }

        public UserModel UserModel { get; set; }
    }
}