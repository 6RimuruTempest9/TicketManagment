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
    public class PurchaseService : IServiceAsync<Purchase>
    {
        private readonly IAsyncRepository<Purchase> _purchaseRepository;

        private readonly ExceptionHelper _exceptionHelper;

        public PurchaseService(IAsyncRepository<Purchase> purchaseRepository, ExceptionHelper exceptionHelper)
        {
            _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));

            _exceptionHelper = exceptionHelper ?? throw new ArgumentNullException(nameof(exceptionHelper));
        }

        public async Task DeleteAsync(int id)
        {
            if (!await IsExistPurchaseByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Purchase));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _purchaseRepository.DeleteAsync(id), "Error during model deletion.");
        }

        public IQueryable<Purchase> GetAll()
        {
            return _exceptionHelper.ExecuteWithServiceException(() =>
                _purchaseRepository.GetAll(), "Error during retrieval of all models.");
        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            if (!await IsExistPurchaseByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Purchase));
            }

            return await _exceptionHelper.ExecuteWithServiceException(() =>
                _purchaseRepository.GetByIdAsync(id), "Error while retrieving model by ID.");
        }

        public Task InsertAsync(Purchase model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return InsertInternalAsync(model);
        }

        private async Task InsertInternalAsync(Purchase model)
        {
            await _exceptionHelper.ExecuteWithServiceException(() =>
                _purchaseRepository.InsertAsync(model), "An error occurred while inserting the model.");
        }

        public Task UpdateAsync(int id, Purchase model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return UpdateInternalAsync(id, model);
        }

        private async Task UpdateInternalAsync(int id, Purchase model)
        {
            if (!await IsExistPurchaseByIdAsync(id))
            {
                throw new ModelNotFoundException(nameof(Purchase));
            }

            await _exceptionHelper.ExecuteWithServiceException(() =>
                _purchaseRepository.UpdateAsync(model), "An error occurred while updating the model.");
        }

        private async Task<bool> IsExistPurchaseByIdAsync(int id)
        {
            var purchases = _purchaseRepository.GetAll();

            return await purchases.AnyAsync(purchase => purchase.Id == id);
        }
    }
}