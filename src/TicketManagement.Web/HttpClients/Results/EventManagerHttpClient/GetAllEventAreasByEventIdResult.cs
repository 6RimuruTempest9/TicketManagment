using System.Collections.Generic;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.HttpClients.Results.EventManagerHttpClient
{
    public class GetAllEventAreasByEventIdResult : Result
    {
        public GetAllEventAreasByEventIdResult(ResultType resultType)
            : base(resultType)
        {
        }

        public IEnumerable<EventAreaModel> EventAreaModels { get; set; }
    }
}