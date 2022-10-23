using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.CommonElements.Exceptions.Models;
using TicketManagement.DataAccess.Repositories;
using TicketManagement.EventManagerApi.Models;

namespace TicketManagement.EventManagerApi.Services.Validation.Validators
{
    public class EventValidator : IAsyncValidator<Event>
    {
        private readonly IAsyncRepository<DataAccess.Entities.Area> _areaRepository;
        private readonly IAsyncRepository<DataAccess.Entities.Seat> _seatRepository;
        private readonly IAsyncRepository<DataAccess.Entities.Event> _eventRepository;
        private readonly IAsyncRepository<DataAccess.Entities.EventArea> _eventAreaRepository;
        private readonly IAsyncRepository<DataAccess.Entities.EventSeat> _eventSeatRepository;

        public EventValidator(
            IAsyncRepository<DataAccess.Entities.Area> areaRepository,
            IAsyncRepository<DataAccess.Entities.Seat> seatRepository,
            IAsyncRepository<DataAccess.Entities.Event> eventRepository,
            IAsyncRepository<DataAccess.Entities.EventArea> eventAreaRepository,
            IAsyncRepository<DataAccess.Entities.EventSeat> eventSeatRepository)
        {
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _eventAreaRepository = eventAreaRepository ?? throw new ArgumentNullException(nameof(eventAreaRepository));
            _eventSeatRepository = eventSeatRepository ?? throw new ArgumentNullException(nameof(eventSeatRepository));
        }

        public async Task ValidateToCreateAsync(Event model)
        {
            if (!await HasAnySeatsAsync(model))
            {
                throw new ModelException("Event can not be created without any seats.", nameof(Event));
            }

            if (await IsExistEventWithDateAsync(model))
            {
                throw new ModelException("Event with this dates is already exist.", nameof(Event));
            }
        }

        public async Task ValidateToDeleteAsync(int id)
        {
            if (!await IsAllSeatsAvailableToPurchase(id))
            {
                throw new ModelException("Event has bought seats.", nameof(Event));
            }
        }

        public async Task ValidateToUpdateAsync(Event model)
        {
            if (!await HasAnySeatsAsync(model))
            {
                throw new ModelException("Event can not be updated without any seats.", nameof(Event));
            }

            if (await IsExistEventWithDateAsync(model))
            {
                throw new ModelException("Event with this dates is already exist.", nameof(Event));
            }
        }

        private async Task<bool> HasAnySeatsAsync(Event @event)
        {
            var hasAnySeats = false;

            var areas = _areaRepository.GetAll().Where(area => area.LayoutId == @event.LayoutId);

            if (await areas.AnyAsync())
            {
                var seats = _seatRepository.GetAll().Where(seat => areas.Any(area => area.Id == seat.AreaId));

                if (await seats.AnyAsync())
                {
                    hasAnySeats = true;
                }
            }

            return hasAnySeats;
        }

        private async Task<bool> IsExistEventWithDateAsync(Event @event)
        {
            return await _eventRepository
                .GetAll()
                .Where(existedEvent => existedEvent.Id != @event.Id)
                .AnyAsync(existedEvent =>
                ((existedEvent.StartDate <= @event.StartDate) && (@event.StartDate <= existedEvent.EndDate))
                || ((existedEvent.StartDate <= @event.EndDate) && (@event.EndDate <= existedEvent.EndDate)));
        }

        private async Task<bool> IsAllSeatsAvailableToPurchase(int eventId)
        {
            var @event = await _eventRepository.GetByIdAsync(eventId);

            var isAllSeatsAvailableToPurchase = true;

            var eventAreas = _eventAreaRepository.GetAll().Where(eventArea => eventArea.EventId == @event.Id);

            if (await eventAreas.AnyAsync())
            {
                var eventSeats = _eventSeatRepository.GetAll().Where(eventSeat =>
                    eventAreas.Any(eventArea => eventArea.Id == eventSeat.EventAreaId));

                if (await eventSeats.AnyAsync(eventSeat => eventSeat.State == DataAccess.Entities.EventSeatState.Busy))
                {
                    isAllSeatsAvailableToPurchase = false;
                }
            }

            return isAllSeatsAvailableToPurchase;
        }
    }
}