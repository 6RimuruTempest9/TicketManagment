using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using TicketManagement.Web.FeatureFlags;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Results;

namespace TicketManagement.Web.Controllers
{
    public class EventSeatController : Controller
    {
        private readonly EventManagerHttpClient _eventManagerHttpClient;

        public EventSeatController(EventManagerHttpClient eventManagerHttpClient)
        {
            _eventManagerHttpClient = eventManagerHttpClient;
        }

        [HttpGet]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> GetAllByEventAreaId(int eventAreaId, decimal ticketPrice)
        {
            var result = await _eventManagerHttpClient.GetAllEventSeatsByEventAreaIdAsync(eventAreaId);

            if (result.ResultType == ResultType.Failure)
            {
                return BadRequest();
            }

            var updatedEventSeats = result.EventSeatModels.Select(eventSeat =>
            {
                eventSeat.TicketPrice = ticketPrice;

                return eventSeat;
            });

            return View(updatedEventSeats);
        }
    }
}