using System.Collections.Generic;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.HttpClients.Results.EventManagerHttpClient
{
    public class GetAllEventSeatsByEventAreaIdResult : Result
    {
        public GetAllEventSeatsByEventAreaIdResult(ResultType resultType)
            : base(resultType)
        {
        }

        public IEnumerable<EventSeatModel> EventSeatModels { get; set; }
    }
}