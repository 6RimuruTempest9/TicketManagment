using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.BusinessLogic.DataTransferObjects;
using TicketManagement.CommonElements.Exceptions.Helpers;
using TicketManagement.CommonElements.Exceptions.Models;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public sealed class LayoutService : IServiceAsync<Layout>
    {
        private readonly IAsyncRepository<Layout> _layoutRepository;
        private readonly IAsyncRepository<Venue> _venueRepository;
        private readonly IAsyncRepository<Area> _areaRepository;
        private readonly IAsyncRepository<Seat> _seatRepository;

        private readonly ExceptionHelper _exceptionHelper;

        public LayoutService(IAsyncRepository<Layout> layoutRepository, ExceptionHelper exceptionHelper,
            IAsyncRepository<Venue> venueRepository,
            IAsyncRepository<Area> areaRepository,
            IAsyncRepository<Seat> seatRepository)
        {
            _layoutRepository = layoutRepository ?? throw new ArgumentNullException(nameof(layoutRepository));
            _venueRepository = venueRepository ?? throw new ArgumentNullException(nameof(venueRepository));
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));

            _exceptionHelper = exceptionHelper ?? throw new ArgumentNullException(nameof(exceptionHelper));
        }

        public async Task DeleteAsync(int id)
        {
            if (!await IsExistLayoutByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Layout));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _layoutRepository.DeleteAsync(id), "Error during model deletion.");
        }

        public IQueryable<Layout> GetAll()
        {
            return _exceptionHelper.ExecuteWithServiceException(() =>
                _layoutRepository.GetAll(), "Error during retrieval of all models.");
        }

        public async Task<Layout> GetByIdAsync(int id)
        {
            if (!await IsExistLayoutByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Layout));
            }

            return await _exceptionHelper.ExecuteWithServiceException(() =>
                _layoutRepository.GetByIdAsync(id), "Error while retrieving model by ID.");
        }

        public Task InsertAsync(Layout model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ModelException("Description should not be empty.", nameof(Layout));
            }

            return InsertInternalAsync(model);
        }

        private async Task InsertInternalAsync(Layout model)
        {
            if (!await HasUniqueDescriptionInVenueAsync(model))
            {
                throw new ModelException("Layout should has unique name.", nameof(Layout));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _layoutRepository.InsertAsync(model), "An error occurred while inserting the model.");
        }

        public Task UpdateAsync(int id, Layout model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ModelException("Description should not be empty.", nameof(Layout));
            }

            return UpdateInternalAsync(id, model);
        }

        private async Task UpdateInternalAsync(int id, Layout model)
        {
            if (!await IsExistLayoutByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Layout));
            }

            if (!await HasUniqueDescriptionInVenueAsync(model))
            {
                throw new ModelException("Layout should has unique name.", nameof(Layout));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _layoutRepository.UpdateAsync(model), "An error occurred while updating the model.");
        }

        private async Task<bool> IsExistLayoutByIdAsync(int id)
        {
            var layouts = _layoutRepository.GetAll();

            return await layouts.AnyAsync(layout => layout.Id == id);
        }

        private async Task<bool> HasUniqueDescriptionInVenueAsync(Layout model)
        {
            return !await _layoutRepository.GetAll()
                .Where(layout => layout.VenueId == model.VenueId)
                .AnyAsync(layout => layout.Description == model.Description);
        }

        public IEnumerable<GroupedAvailableLayoutModelDto> GetGroupedAvailableLayoutModel()
        {
            var venues = _venueRepository.GetAll();
            var layouts = _layoutRepository.GetAll();
            var areas = _areaRepository.GetAll();
            var seats = _seatRepository.GetAll();

            var query =
                from venue in venues
                join layout in layouts on venue.Id equals layout.VenueId
                join area in areas on layout.Id equals area.LayoutId
                join seat in seats on area.Id equals seat.AreaId
                select new
                {
                    Venue = venue,
                    Layout = layout,
                };

            var models = query
                .Distinct()
                .AsEnumerable()
                .GroupBy(x => x.Venue, x => x.Layout)
                .Select(x => new GroupedAvailableLayoutModelDto
                {
                    Venue = x.Key,
                    Layouts = x,
                });

            return models;
        }
    }
}