using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Repositories;
using TicketManagement.EventManagerApi.Models;
using TicketManagement.EventManagerApi.Services.Validation.Validators;

namespace TicketManagement.EventManagerApi.Services
{
    public class EventService : IAsyncService<Event>
    {
        private readonly IAsyncRepository<DataAccess.Entities.Event> _eventRepository;
        private readonly IAsyncValidator<Event> _eventValidator;

        public EventService(IAsyncRepository<DataAccess.Entities.Event> eventRepository,
            IAsyncValidator<Event> eventValidator)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _eventValidator = eventValidator ?? throw new ArgumentNullException(nameof(eventValidator));
        }

        public async Task CreateAsync(Event model)
        {
            await _eventValidator.ValidateToCreateAsync(model);

            var eventEntity = new DataAccess.Entities.Event
            {
                Id = model.Id,
                LayoutId = model.LayoutId,
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ImageUrl = model.ImageUrl,
            };

            await _eventRepository.InsertAsync(eventEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _eventValidator.ValidateToDeleteAsync(id);

            await _eventRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            var eventEntities = await _eventRepository.GetAll().ToListAsync();

            var eventModels = new List<Event>();

            foreach (var eventEntity in eventEntities)
            {
                var eventModel = new Event
                {
                    Id = eventEntity.Id,
                    LayoutId = eventEntity.LayoutId,
                    Name = eventEntity.Name,
                    Description = eventEntity.Description,
                    StartDate = eventEntity.StartDate,
                    EndDate = eventEntity.EndDate,
                    ImageUrl = eventEntity.ImageUrl,
                };

                eventModels.Add(eventModel);
            }

            return eventModels;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            var eventModel = new Event
            {
                Id = eventEntity.Id,
                LayoutId = eventEntity.LayoutId,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
                ImageUrl = eventEntity.ImageUrl,
            };

            return eventModel;
        }

        public async Task UpdateAsync(Event model)
        {
            await _eventValidator.ValidateToUpdateAsync(model);

            var eventEntity = new DataAccess.Entities.Event
            {
                Id = model.Id,
                LayoutId = model.LayoutId,
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ImageUrl = model.ImageUrl,
            };

            await _eventRepository.UpdateAsync(eventEntity);
        }
    }
}