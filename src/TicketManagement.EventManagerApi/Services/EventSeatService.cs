using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Repositories;
using TicketManagement.EventManagerApi.Models;

namespace TicketManagement.EventManagerApi.Services
{
    public class EventSeatService : IAsyncService<EventSeat>
    {
        private readonly IAsyncRepository<DataAccess.Entities.EventSeat> _eventSeatRepository;

        public EventSeatService(IAsyncRepository<DataAccess.Entities.EventSeat> eventSeatRepository)
        {
            _eventSeatRepository = eventSeatRepository ?? throw new ArgumentNullException(nameof(eventSeatRepository));
        }

        public Task CreateAsync(EventSeat model)
        {
            var eventSeatEntity = new DataAccess.Entities.EventSeat
            {
                Id = model.Id,
                EventAreaId = model.EventAreaId,
                Number = model.Number,
                Row = model.Row,
                State = model.State,
            };

            return _eventSeatRepository.InsertAsync(eventSeatEntity);
        }

        public Task DeleteAsync(int id)
        {
            return _eventSeatRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EventSeat>> GetAllAsync()
        {
            var eventSeatEntities = await _eventSeatRepository.GetAll().ToListAsync();

            var eventSeatModels = new List<EventSeat>();

            foreach (var eventSeatEntity in eventSeatEntities)
            {
                var eventModel = new EventSeat
                {
                    Id = eventSeatEntity.Id,
                    EventAreaId = eventSeatEntity.EventAreaId,
                    Number = eventSeatEntity.Number,
                    Row = eventSeatEntity.Row,
                    State = eventSeatEntity.State,
                };

                eventSeatModels.Add(eventModel);
            }

            return eventSeatModels;
        }

        public async Task<EventSeat> GetByIdAsync(int id)
        {
            var eventSeatEntity = await _eventSeatRepository.GetByIdAsync(id);

            var eventSeatModel = new EventSeat
            {
                Id = eventSeatEntity.Id,
                EventAreaId = eventSeatEntity.EventAreaId,
                Number = eventSeatEntity.Number,
                Row = eventSeatEntity.Row,
                State = eventSeatEntity.State,
            };

            return eventSeatModel;
        }

        public Task UpdateAsync(EventSeat model)
        {
            var eventSeatEntity = new DataAccess.Entities.EventSeat
            {
                Id = model.Id,
                EventAreaId = model.EventAreaId,
                Number = model.Number,
                Row = model.Row,
                State = model.State,
            };

            return _eventSeatRepository.UpdateAsync(eventSeatEntity);
        }
    }
}