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
    [Route("eventSeat")]
    [ApiController]
    public class EventSeatController : ControllerBase
    {
        private readonly IAsyncService<EventSeat> _eventSeatService;

        public EventSeatController(IAsyncService<EventSeat> eventSeatService)
        {
            _eventSeatService = eventSeatService ?? throw new ArgumentNullException(nameof(eventSeatService));
        }

        /// <summary>
        /// Get all event seats by event area id.
        /// </summary>
        /// <param name="eventAreaId">Event area id.</param>
        /// <response code="200">Event seats were found.</response>
        [HttpGet]
        [Route("getAllByEventAreaId/{eventAreaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByEventAreaId(int eventAreaId)
        {
            var eventSeats = await _eventSeatService.GetAllAsync();

            return Ok(eventSeats.Where(eventSeat => eventSeat.EventAreaId == eventAreaId));
        }

        /// <summary>
        /// Update event seat state. Event seat getting by id.
        /// </summary>
        /// <param name="eventSeatId">Event seat id.</param>
        /// <param name="eventSeatState">Event seat state.</param>
        /// <response code="200">Event seat state was updated.</response>
        [HttpPost]
        [Route("updateEventSeatStateById/{eventSeatId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEventSeatStateById(
            int eventSeatId,
            [FromForm] DataAccess.Entities.EventSeatState eventSeatState)
        {
            var eventSeat = await _eventSeatService.GetByIdAsync(eventSeatId);

            eventSeat.State = eventSeatState;

            await _eventSeatService.UpdateAsync(eventSeat);

            return Ok();
        }
    }
}