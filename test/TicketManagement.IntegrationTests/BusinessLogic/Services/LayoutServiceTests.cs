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
    public class LayoutServiceTests : ServiceTestBase
    {
        private IServiceAsync<Layout> _layoutService;
        private IAsyncRepository<Layout> _layoutRepository;

        [SetUp]
        public void Setup()
        {
            _layoutService = Provider.GetService(typeof(IServiceAsync<Layout>)) as IServiceAsync<Layout>;
            _layoutRepository = Provider.GetService(typeof(IAsyncRepository<Layout>)) as IAsyncRepository<Layout>;
        }

        [Test]
        public async Task GetAll_WhenCallIt_ShouldReturnListOfModels()
        {
            // Arrange
            var modelsCount = await _layoutRepository.GetAll().CountAsync();

            // Act
            var models = await _layoutService.GetAll().ToListAsync();

            // Assert
            Assert.That(models, Is.TypeOf(typeof(List<Layout>)));
            Assert.That(models.Count, Is.EqualTo(modelsCount));
        }

        [Test]
        public async Task GetByIdAsync_WhenExistId_ShouldReturnModel()
        {
            // Arrange
            var models = _layoutRepository.GetAll();

            if (!await models.AnyAsync())
            {
                Assert.Pass("Count of models is zero.");
            }

            var expectedModel = await models.FirstAsync();

            // Act
            var actualModel = await _layoutService.GetByIdAsync(expectedModel.Id);

            // Assert
            Assert.That(actualModel, Is.EqualTo(expectedModel));
        }

        [Test]
        public void GetByIdAsync_WhenNonExistId_ShouldThrowServiceException()
        {
            // Arrange
            var nonExistModelId = -1;

            // Act
            AsyncTestDelegate action = () => _layoutService.GetByIdAsync(nonExistModelId);

            // Assert
            Assert.ThrowsAsync<ModelNotFoundException>(action);
        }
    }
}