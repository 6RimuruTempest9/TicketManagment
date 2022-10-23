using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.EventManagerApi.Attributes.Authorization;
using TicketManagement.EventManagerApi.Models;
using TicketManagement.EventManagerApi.Services;

namespace TicketManagement.EventManagerApi.Controllers
{
    [Route("event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IAsyncService<Event> _eventService;

        public EventController(IAsyncService<Event> eventService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        /// <summary>
        /// Create event.
        /// </summary>
        /// <param name="event">Event model.</param>
        /// <response code="200">Event was successfully created.</response>
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateEvent([FromForm] Event @event)
        {
            await _eventService.CreateAsync(@event);

            return Ok();
        }

        /// <summary>
        /// Update event.
        /// </summary>
        /// <param name="event">Event model.</param>
        /// <response code="200">Event was successfully updated.</response>
        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEvent([FromForm] Event @event)
        {
            await _eventService.UpdateAsync(@event);

            return Ok();
        }

        /// <summary>
        /// Delete event by id.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <response code="200">Event was successfully deleted.</response>
        [HttpGet]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteAsync(id);

            return Ok();
        }

        /// <summary>
        /// Get event model by id.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <response code="200">Event was found.</response>
        [HttpGet]
        [Route("getById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventById(int id)
        {
            var @event = await _eventService.GetByIdAsync(id);

            return Ok(@event);
        }

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <response code="200">Events were found.</response>
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllAsync();

            return Ok(events);
        }
    }
}