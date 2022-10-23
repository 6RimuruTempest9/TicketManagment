using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Repositories;
using TicketManagement.EventManagerApi.Models;

namespace TicketManagement.EventManagerApi.Services
{
    public class EventAreaService : IAsyncService<EventArea>
    {
        private readonly IAsyncRepository<DataAccess.Entities.EventArea> _eventAreaRepository;

        public EventAreaService(IAsyncRepository<DataAccess.Entities.EventArea> eventAreaRepository)
        {
            _eventAreaRepository = eventAreaRepository ?? throw new ArgumentNullException(nameof(eventAreaRepository));
        }

        public Task CreateAsync(EventArea model)
        {
            var eventAreaEntity = new DataAccess.Entities.EventArea
            {
                Id = model.Id,
                EventId = model.EventId,
                Description = model.Description,
                CoordX = model.CoordX,
                CoordY = model.CoordY,
                Price = model.Price,
            };

            return _eventAreaRepository.InsertAsync(eventAreaEntity);
        }

        public Task DeleteAsync(int id)
        {
            return _eventAreaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EventArea>> GetAllAsync()
        {
            var eventAreaEntities = await _eventAreaRepository.GetAll().ToListAsync();

            var eventAreaModels = new List<EventArea>();

            foreach (var eventAreaEntity in eventAreaEntities)
            {
                var eventModel = new EventArea
                {
                    Id = eventAreaEntity.Id,
                    EventId = eventAreaEntity.EventId,
                    Description = eventAreaEntity.Description,
                    CoordX = eventAreaEntity.CoordX,
                    CoordY = eventAreaEntity.CoordY,
                    Price = eventAreaEntity.Price,
                };

                eventAreaModels.Add(eventModel);
            }

            return eventAreaModels;
        }

        public async Task<EventArea> GetByIdAsync(int id)
        {
            var eventAreaEntity = await _eventAreaRepository.GetByIdAsync(id);

            var eventAreaModel = new EventArea
            {
                Id = eventAreaEntity.Id,
                EventId = eventAreaEntity.EventId,
                Description = eventAreaEntity.Description,
                CoordX = eventAreaEntity.CoordX,
                CoordY = eventAreaEntity.CoordY,
                Price = eventAreaEntity.Price,
            };

            return eventAreaModel;
        }

        public Task UpdateAsync(EventArea model)
        {
            var eventAreaEntity = new DataAccess.Entities.EventArea
            {
                Id = model.Id,
                EventId = model.EventId,
                Description = model.Description,
                CoordX = model.CoordX,
                CoordY = model.CoordY,
                Price = model.Price,
            };

            return _eventAreaRepository.UpdateAsync(eventAreaEntity);
        }
    }
}