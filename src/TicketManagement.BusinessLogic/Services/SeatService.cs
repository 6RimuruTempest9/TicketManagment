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
    internal sealed class SeatService : IServiceAsync<Seat>
    {
        private readonly IAsyncRepository<Seat> _seatRepository;

        private readonly ExceptionHelper _exceptionHelper;

        public SeatService(IAsyncRepository<Seat> seatRepository, ExceptionHelper exceptionHelper)
        {
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));

            _exceptionHelper = exceptionHelper ?? throw new ArgumentNullException(nameof(exceptionHelper));
        }

        public async Task DeleteAsync(int id)
        {
            if (!await IsExistSeatByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Seat));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _seatRepository.DeleteAsync(id), "Error during model deletion.");
        }

        public IQueryable<Seat> GetAll()
        {
            return _exceptionHelper.ExecuteWithServiceException(() =>
                _seatRepository.GetAll(), "Error during retrieval of all models.");
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            if (!await IsExistSeatByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Seat));
            }

            return await _exceptionHelper.ExecuteWithServiceException(() =>
                _seatRepository.GetByIdAsync(id), "Error while retrieving model by ID.");
        }

        public Task InsertAsync(Seat model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return InsertInternalAsync(model);
        }

        private async Task InsertInternalAsync(Seat model)
        {
            if (!await IsUniqueRowAndNumberAsync(model))
            {
                throw new ModelException("Seat should has unique row and number.", nameof(Seat));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _seatRepository.InsertAsync(model), "An error occurred while inserting the model.");
        }

        public Task UpdateAsync(int id, Seat model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return UpdateInternalAsync(id, model);
        }

        private async Task UpdateInternalAsync(int id, Seat model)
        {
            if (!await IsExistSeatByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Seat));
            }

            if (!await IsUniqueRowAndNumberAsync(model))
            {
                throw new ModelException("Seat should has unique row and number.", nameof(Seat));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _seatRepository.UpdateAsync(model), "An error occurred while updating the model.");
        }

        private async Task<bool> IsExistSeatByIdAsync(int id)
        {
            var seats = _seatRepository.GetAll();

            return await seats.AnyAsync(seat => seat.Id == id);
        }

        private async Task<bool> IsUniqueRowAndNumberAsync(Seat model)
        {
            return !await _seatRepository.GetAll()
                .Where(seat => seat.AreaId == model.AreaId)
                .AnyAsync(seat => (seat.Row == model.Row) && (seat.Number == model.Number));
        }
    }
}