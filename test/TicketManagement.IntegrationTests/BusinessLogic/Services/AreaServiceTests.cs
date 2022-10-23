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
    public class AreaServiceTests : ServiceTestBase
    {
        private IServiceAsync<Area> _areaService;
        private IAsyncRepository<Area> _areaRepository;

        [SetUp]
        public void Setup()
        {
            _areaService = Provider.GetService(typeof(IServiceAsync<Area>)) as IServiceAsync<Area>;
            _areaRepository = Provider.GetService(typeof(IAsyncRepository<Area>)) as IAsyncRepository<Area>;
        }

        [Test]
        public async Task GetAll_WhenCallIt_ShouldReturnListOfModels()
        {
            // Arrange
            var modelsCount = await _areaRepository.GetAll().CountAsync();

            // Act
            var models = await _areaService.GetAll().ToListAsync();

            // Assert
            Assert.That(models, Is.TypeOf(typeof(List<Area>)));
            Assert.That(models.Count, Is.EqualTo(modelsCount));
        }

        [Test]
        public async Task GetByIdAsync_WhenExistId_ShouldReturnModel()
        {
            // Arrange
            var models = _areaRepository.GetAll();

            if (!await models.AnyAsync())
            {
                Assert.Pass("Count of models is zero.");
            }

            var expectedModel = await models.FirstAsync();

            // Act
            var actualModel = await _areaService.GetByIdAsync(expectedModel.Id);

            // Assert
            Assert.That(actualModel, Is.EqualTo(expectedModel));
        }

        [Test]
        public void GetByIdAsync_WhenNonExistId_ShouldThrowServiceException()
        {
            // Arrange
            var nonExistModelId = -1;

            // Act
            AsyncTestDelegate action = () => _areaService.GetByIdAsync(nonExistModelId);

            // Assert
            Assert.ThrowsAsync<ModelNotFoundException>(action);
        }
    }
}