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
    public class SeatServiceTests : ServiceTestBase
    {
        private IServiceAsync<Seat> _seatService;
        private IAsyncRepository<Seat> _seatRepository;

        [SetUp]
        public void Setup()
        {
            _seatService = Provider.GetService(typeof(IServiceAsync<Seat>)) as IServiceAsync<Seat>;
            _seatRepository = Provider.GetService(typeof(IAsyncRepository<Seat>)) as IAsyncRepository<Seat>;
        }

        [Test]
        public async Task GetAll_WhenCallIt_ShouldReturnListOfModels()
        {
            // Arrange
            var modelsCount = await _seatRepository.GetAll().CountAsync();

            // Act
            var models = await _seatService.GetAll().ToListAsync();

            // Assert
            Assert.That(models, Is.TypeOf(typeof(List<Seat>)));
            Assert.That(models.Count, Is.EqualTo(modelsCount));
        }

        [Test]
        public async Task GetByIdAsync_WhenExistId_ShouldReturnModel()
        {
            // Arrange
            var models = _seatRepository.GetAll();

            if (!await models.AnyAsync())
            {
                Assert.Pass("Count of models is zero.");
            }

            var expectedModel = await models.FirstAsync();

            // Act
            var actualModel = await _seatService.GetByIdAsync(expectedModel.Id);

            // Assert
            Assert.That(actualModel, Is.EqualTo(expectedModel));
        }

        [Test]
        public void GetByIdAsync_WhenNonExistId_ShouldThrowServiceException()
        {
            // Arrange
            var nonExistModelId = -1;

            // Act
            AsyncTestDelegate action = () => _seatService.GetByIdAsync(nonExistModelId);

            // Assert
            Assert.ThrowsAsync<ModelNotFoundException>(action);
        }
    }
}