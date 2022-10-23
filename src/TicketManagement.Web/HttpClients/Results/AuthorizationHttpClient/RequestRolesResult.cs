using System.Collections.Generic;

namespace TicketManagement.Web.HttpClients.Results.AuthorizationHttpClient
{
    public class RequestRolesResult : Result
    {
        public RequestRolesResult(ResultType resultType)
            : base(resultType)
        {
        }

        public IEnumerable<string> Roles { get; set; }
    }
}