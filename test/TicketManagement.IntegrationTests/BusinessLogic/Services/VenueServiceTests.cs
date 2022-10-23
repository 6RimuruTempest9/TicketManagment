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
    public class VenueServiceTests : ServiceTestBase
    {
        private IServiceAsync<Venue> _venueService;
        private IAsyncRepository<Venue> _repositoryVenue;

        [SetUp]
        public void Setup()
        {
            _venueService = Provider.GetService(typeof(IServiceAsync<Venue>)) as IServiceAsync<Venue>;
            _repositoryVenue = Provider.GetService(typeof(IAsyncRepository<Venue>)) as IAsyncRepository<Venue>;
        }

        [Test]
        public async Task GetAll_WhenCallIt_ShouldReturnListOfModels()
        {
            // Arrange
            var modelsCount = await _repositoryVenue.GetAll().CountAsync();

            // Act
            var models = await _venueService.GetAll().ToListAsync();

            // Assert
            Assert.That(models, Is.TypeOf(typeof(List<Venue>)));
            Assert.That(models.Count, Is.EqualTo(modelsCount));
        }

        [Test]
        public async Task GetByIdAsync_WhenExistId_ShouldReturnModel()
        {
            // Arrange
            var models = _repositoryVenue.GetAll();

            if (!await models.AnyAsync())
            {
                Assert.Pass("Count of models is zero.");
            }

            var expectedModel = await models.FirstAsync();

            // Act
            var actualModel = await _venueService.GetByIdAsync(expectedModel.Id);

            // Assert
            Assert.That(actualModel, Is.EqualTo(expectedModel));
        }

        [Test]
        public void GetByIdAsync_WhenNonExistId_ShouldThrowServiceException()
        {
            // Arrange
            var nonExistModelId = -1;

            // Act
            AsyncTestDelegate action = () => _venueService.GetByIdAsync(nonExistModelId);

            // Assert
            Assert.ThrowsAsync<ModelNotFoundException>(action);
        }
    }
}