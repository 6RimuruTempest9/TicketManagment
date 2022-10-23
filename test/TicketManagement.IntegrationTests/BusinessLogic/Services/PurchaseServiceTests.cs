using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.CommonElements.Exceptions.Models;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.BusinessLogic.Services
{
    public class PurchaseServiceTests : ServiceTestBase
    {
        private IServiceAsync<Purchase> _purchaseService;
        private IAsyncRepository<Purchase> _purchaseRepository;

        [SetUp]
        public void Setup()
        {
            _purchaseService = Provider.GetService(typeof(IServiceAsync<Purchase>)) as IServiceAsync<Purchase>;
            _purchaseRepository = Provider.GetService(typeof(IAsyncRepository<Purchase>)) as IAsyncRepository<Purchase>;
        }

        [Test]
        public async Task GetAll_WhenCallIt_ShouldReturnListOfModels()
        {
            // Arrange
            var modelsCount = await _purchaseRepository.GetAll().CountAsync();

            // Act
            var models = await _purchaseService.GetAll().ToListAsync();

            // Assert
            Assert.That(models, Is.TypeOf(typeof(List<Purchase>)));
            Assert.That(models.Count, Is.EqualTo(modelsCount));
        }

        [Test]
        public async Task GetByIdAsync_WhenExistId_ShouldReturnModel()
        {
            // Arrange
            var models = _purchaseRepository.GetAll();

            if (!await models.AnyAsync())
            {
                Assert.Pass("Count of models is zero.");
            }

            var expectedModel = await models.FirstAsync();

            // Act
            var actualModel = await _purchaseService.GetByIdAsync(expectedModel.Id);

            // Assert
            Assert.That(actualModel, Is.EqualTo(expectedModel));
        }

        [Test]
        public void GetByIdAsync_WhenNonExistId_ShouldThrowServiceException()
        {
            // Arrange
            var nonExistModelId = -1;

            // Act
            AsyncTestDelegate action = () => _purchaseService.GetByIdAsync(nonExistModelId);

            // Assert
            Assert.ThrowsAsync<ModelNotFoundException>(action);
        }
    }
}