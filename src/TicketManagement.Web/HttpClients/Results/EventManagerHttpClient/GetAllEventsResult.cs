using System.Collections.Generic;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.HttpClients.Results.EventManagerHttpClient
{
    public class GetAllEventsResult : Result
    {
        public GetAllEventsResult(ResultType resultType)
            : base(resultType)
        {
        }

        public IEnumerable<EventModel> EventModels { get; set; }
    }
}