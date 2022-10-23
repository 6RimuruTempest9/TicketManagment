using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.CommonElements.Exceptions.Helpers;
using TicketManagement.CommonElements.Exceptions.Models;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.UnitTests.BusinessLogic.Services
{
    public class AreaServiceTests
    {
        [Test]
        public void DeleteAsync_WhenInputIdOfNonExistArea_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await areaService.DeleteAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void GetByIdAsync_WhenInputIdOfNonExistArea_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await areaService.GetByIdAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void InsertAsync_WhenInputAreaIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await areaService.InsertAsync(null);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void InsertAsync_WhenInputAreaWithNullDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 3,
                CoordX = 0,
                CoordY = 0,
                Description = null,
            };

            // Act
            Func<Task> action = async () => await areaService.InsertAsync(area);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void InsertAsync_WhenInputAreaWithEmptyDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 3,
                CoordX = 0,
                CoordY = 0,
                Description = string.Empty,
            };

            // Act
            Func<Task> action = async () => await areaService.InsertAsync(area);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void InsertAsync_WhenInputAreaWithExistDescriptionInTheSameLayout_ShouldThrowModelException()
        {
            // Arrange
            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 1,
                CoordX = 0,
                CoordY = 0,
                Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
            };

            // Act
            Func<Task> action = async () => await areaService.InsertAsync(area);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputIdOfNonExistArea_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 3,
                CoordX = 25,
                CoordY = 25,
                Description = "Area 3. LayoutId = 3. CoordX = 25. CoordY = 25.",
            };

            // Act
            Func<Task> action = async () => await areaService.UpdateAsync(id, area);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void UpdateAsync_WhenInputAreaIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var id = 2;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = default(Area);

            // Act
            Func<Task> action = async () => await areaService.UpdateAsync(id, area);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void UpdateAsync_WhenInputAreaWithNullDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 3,
                CoordX = 0,
                CoordY = 0,
                Description = null,
            };

            // Act
            Func<Task> action = async () => await areaService.UpdateAsync(id, area);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputAreaWithEmptyDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 3,
                CoordX = 0,
                CoordY = 0,
                Description = string.Empty,
            };

            // Act
            Func<Task> action = async () => await areaService.UpdateAsync(id, area);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputAreaWithExistDescriptionInTheSameLayout_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var areaRepositoryMock = new Mock<IAsyncRepository<Area>>();

            var areas = new[]
            {
                new Area
                {
                    Id = 1,
                    LayoutId = 1,
                    CoordX = 0,
                    CoordY = 0,
                    Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
                },
                new Area
                {
                    Id = 2,
                    LayoutId = 2,
                    CoordX = 25,
                    CoordY = 25,
                    Description = "Area 2. LayoutId = 2. CoordX = 25. CoordY = 25.",
                },
            };

            areaRepositoryMock.Setup(mock => mock.GetAll()).Returns(areas.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var areaService = new AreaService(areaRepositoryMock.Object, exceptionHelperMock.Object);

            var area = new Area
            {
                Id = 3,
                LayoutId = 1,
                CoordX = 0,
                CoordY = 0,
                Description = "Area 1. LayoutId = 1. CoordX = 0. CoordY = 0.",
            };

            // Act
            Func<Task> action = async () => await areaService.UpdateAsync(id, area);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }
    }
}