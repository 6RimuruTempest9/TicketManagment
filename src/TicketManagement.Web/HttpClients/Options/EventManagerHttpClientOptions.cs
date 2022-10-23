namespace TicketManagement.Web.HttpClients.Options
{
    public class EventManagerHttpClientOptions : HttpClientOptions
    {
        public string CreateEventUrl { get; set; }

        public string UpdateEventUrl { get; set; }

        public string DeleteEventByIdUrl { get; set; }

        public string GetAllEventsUrl { get; set; }

        public string GetEventByIdUrl { get; set; }

        public string GetAllEventAreasByEventIdUrl { get; set; }

        public string GetAllEventSeatsByEventAreaIdUrl { get; set; }

        public string UpdateEventSeatStateByIdUrl { get; set; }
    }
}