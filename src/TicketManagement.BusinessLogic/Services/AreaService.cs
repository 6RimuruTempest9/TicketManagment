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
    internal sealed class AreaService : IServiceAsync<Area>
    {
        private readonly IAsyncRepository<Area> _areaRepository;

        private readonly ExceptionHelper _exceptionHelper;

        public AreaService(IAsyncRepository<Area> areaRepository, ExceptionHelper exceptionHelper)
        {
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));

            _exceptionHelper = exceptionHelper ?? throw new ArgumentNullException(nameof(exceptionHelper));
        }

        public async Task DeleteAsync(int id)
        {
            if (!await IsExistAreaByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Area));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _areaRepository.DeleteAsync(id), "Error during model deletion.");
        }

        public IQueryable<Area> GetAll()
        {
            return _exceptionHelper.ExecuteWithServiceException(() =>
                _areaRepository.GetAll(), "Error during retrieval of all models.");
        }

        public async Task<Area> GetByIdAsync(int id)
        {
            if (!await IsExistAreaByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Area));
            }

            return await _exceptionHelper.ExecuteWithServiceException(() =>
                _areaRepository.GetByIdAsync(id), "Error while retrieving model by ID.");
        }

        public Task InsertAsync(Area model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ModelException("Description should not be empty.", nameof(Area));
            }

            return InsertInternalAsync(model);
        }

        private async Task InsertInternalAsync(Area model)
        {
            if (!await HasUniqueDescriptionInLayoutAsync(model))
            {
                throw new ModelException("Area should has unique description in one layout.", nameof(Area));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _areaRepository.InsertAsync(model), "An error occurred while inserting the model.");
        }

        public Task UpdateAsync(int id, Area model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ModelException("Description should not be empty.", nameof(Area));
            }

            return UpdateInternalAsync(id, model);
        }

        private async Task UpdateInternalAsync(int id, Area model)
        {
            if (!await IsExistAreaByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Area));
            }

            if (!await HasUniqueDescriptionInLayoutAsync(model))
            {
                throw new ModelException("Area should has unique description in one layout.", nameof(Area));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _areaRepository.UpdateAsync(model), "An error occurred while updating the model.");
        }

        private async Task<bool> IsExistAreaByIdAsync(int id)
        {
            var areas = _areaRepository.GetAll();

            return await areas.AnyAsync(area => area.Id == id);
        }

        private async Task<bool> HasUniqueDescriptionInLayoutAsync(Area model)
        {
            return !await _areaRepository.GetAll()
                .Where(area => area.LayoutId == model.LayoutId)
                .AnyAsync(area => area.Description == model.Description);
        }
    }
}