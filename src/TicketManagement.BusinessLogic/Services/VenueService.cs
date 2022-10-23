using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.CommonElements.Exceptions.Helpers;
using TicketManagement.CommonElements.Exceptions.Models;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal sealed class VenueService : IServiceAsync<Venue>
    {
        private readonly IAsyncRepository<Venue> _venueRepository;

        private readonly ExceptionHelper _exceptionHelper;

        public VenueService(IAsyncRepository<Venue> venueRepository, ExceptionHelper exceptionHelper)
        {
            _venueRepository = venueRepository ?? throw new ArgumentNullException(nameof(venueRepository));

            _exceptionHelper = exceptionHelper ?? throw new ArgumentNullException(nameof(exceptionHelper));
        }

        public async Task DeleteAsync(int id)
        {
            if (!await IsExistVenueByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Venue));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _venueRepository.DeleteAsync(id), "Error during model deletion.");
        }

        public IQueryable<Venue> GetAll()
        {
            return _exceptionHelper.ExecuteWithServiceException(() =>
                _venueRepository.GetAll(), "Error during retrieval of all models.");
        }

        public async Task<Venue> GetByIdAsync(int id)
        {
            if (!await IsExistVenueByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Venue));
            }

            return await _exceptionHelper.ExecuteWithServiceException(() =>
                _venueRepository.GetByIdAsync(id), "Error while retrieving model by ID.");
        }

        public Task InsertAsync(Venue model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ModelException("Description should not be empty.", nameof(Venue));
            }

            if (string.IsNullOrEmpty(model.Address))
            {
                throw new ModelException("Address should not be empty.", nameof(Venue));
            }

            return InsertInternalAsync(model);
        }

        private async Task InsertInternalAsync(Venue model)
        {
            if (!await IsUniqueAddressAsync(model))
            {
                throw new ModelException("Venue with this address already exist.", nameof(Venue));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _venueRepository.InsertAsync(model), "An error occurred while inserting the model.");
        }

        public Task UpdateAsync(int id, Venue model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ModelException("Description should not be empty.", nameof(Venue));
            }

            if (string.IsNullOrEmpty(model.Address))
            {
                throw new ModelException("Address should not be empty.", nameof(Venue));
            }

            return UpdateInternalAsync(id, model);
        }

        private async Task UpdateInternalAsync(int id, Venue model)
        {
            if (!await IsExistVenueByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Venue));
            }

            if (!await IsUniqueAddressAsync(model))
            {
                throw new ModelException("Venue with this address already exist.", nameof(Venue));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _venueRepository.UpdateAsync(model), "An error occurred while updating the model.");
        }

        private async Task<bool> IsExistVenueByIdAsync(int id)
        {
            var venues = _venueRepository.GetAll();

            return await venues.AnyAsync(venue => venue.Id == id);
        }

        private async Task<bool> IsUniqueAddressAsync(Venue model)
        {
            return !await _venueRepository.GetAll().AnyAsync(venue => venue.Address == model.Address);
        }
    }
}