using TicketManagement.Web.Models;

namespace TicketManagement.Web.HttpClients.Results.EventManagerHttpClient
{
    public class GetEventByIdResult : Result
    {
        public GetEventByIdResult(ResultType resultType)
            : base(resultType)
        {
        }

        public EventModel EventModel { get; set; }
    }
}