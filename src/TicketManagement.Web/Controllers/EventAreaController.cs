using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using TicketManagement.Web.FeatureFlags;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.HttpClients.Results;

namespace TicketManagement.Web.Controllers
{
    public class EventAreaController : Controller
    {
        private readonly EventManagerHttpClient _eventManagerHttpClient;

        public EventAreaController(EventManagerHttpClient eventManagerHttpClient)
        {
            _eventManagerHttpClient = eventManagerHttpClient;
        }

        [HttpGet]
        [FeatureGate(ReactRedirectFeatureFlags.React)]
        public async Task<IActionResult> GetAllByEventId(int eventId)
        {
            var getAllAreasByEventIdResult = await _eventManagerHttpClient.GetAllEventAreasByEventIdAsync(eventId);

            if (getAllAreasByEventIdResult.ResultType == ResultType.Failure)
            {
                return BadRequest();
            }

            return View(getAllAreasByEventIdResult.EventAreaModels);
        }
    }
}