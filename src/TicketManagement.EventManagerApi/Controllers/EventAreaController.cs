using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.EventManagerApi.Models;
using TicketManagement.EventManagerApi.Services;

namespace TicketManagement.EventManagerApi.Controllers
{
    [Route("eventArea")]
    [ApiController]
    public class EventAreaController : ControllerBase
    {
        private readonly IAsyncService<EventArea> _eventAreaService;

        public EventAreaController(IAsyncService<EventArea> eventAreaService)
        {
            _eventAreaService = eventAreaService ?? throw new ArgumentNullException(nameof(eventAreaService));
        }

        /// <summary>
        /// Get all "EventArea" models that has same "EventId" property value.
        /// </summary>
        /// <param name="eventId">Event id.</param>
        /// <response code="200">"EventArea" models was found.</response>
        [HttpGet]
        [Route("getAllByEventId/{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByEventId(int eventId)
        {
            var eventAreas = await _eventAreaService.GetAllAsync();

            return Ok(eventAreas.Where(eventArea => eventArea.EventId == eventId));
        }
    }
}