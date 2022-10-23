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
    public class VenueServiceTests
    {
        [Test]
        public void DeleteAsync_WhenInputIdOfNonExistVenue_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await venueService.DeleteAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void GetByIdAsync_WhenInputIdOfNonExistVenue_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await venueService.GetByIdAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void InsertAsync_WhenInputVenueIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await venueService.InsertAsync(null);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void InsertAsync_WhenInputVenueWithNullDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = "Address of Venue 2.",
                Description = null,
            };

            // Act
            Func<Task> action = async () => await venueService.InsertAsync(venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void InsertAsync_WhenInputVenueWithEmptyDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = "Address of Venue 2.",
                Description = string.Empty,
            };

            // Act
            Func<Task> action = async () => await venueService.InsertAsync(venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void InsertAsync_WhenInputVenueWithNullAddressProperty_ShouldThrowModelException()
        {
            // Arrange
            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = null,
                Description = "Venue 3.",
            };

            // Act
            Func<Task> action = async () => await venueService.InsertAsync(venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void InsertAsync_WhenInputVenueWithEmptyAddressProperty_ShouldThrowModelException()
        {
            // Arrange
            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = string.Empty,
                Description = "Venue 3.",
            };

            // Act
            Func<Task> action = async () => await venueService.InsertAsync(venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void InsertAsync_WhenInputVenueHasAlreadyExistAddressProperty_ShouldThrowModelException()
        {
            // Arrange
            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = "Address of Venue 2.",
                Description = "Venue 3.",
            };

            // Act
            Func<Task> action = async () => await venueService.InsertAsync(venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputIdOfNonExistVenue_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 1,
                Phone = "1234567892",
                Address = "Address of Venue 1.",
                Description = "Venue 1.",
            };

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void UpdateAsync_WhenInputVenueIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var id = 2;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = default(Venue);

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void UpdateAsync_WhenInputVenueWithNullDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = "Address of Venue 2.",
                Description = null,
            };

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputVenueWithEmptyDescriptionProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = "Address of Venue 2.",
                Description = string.Empty,
            };

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputVenueWithNullAddressProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = null,
                Description = "Venue 3.",
            };

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputVenueWithEmptyAddressProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 3,
                Phone = "1234567891",
                Address = string.Empty,
                Description = "Venue 3.",
            };

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputVenueHasAlreadyExistAddressProperty_ShouldThrowModelException()
        {
            // Arrange
            var id = 1;

            var venueRepositoryMock = new Mock<IAsyncRepository<Venue>>();

            var venues = new[]
            {
                new Venue
                {
                    Id = 1,
                    Phone = "1234567890",
                    Address = "Address of Venue 1.",
                    Description = "Venue 1.",
                },
                new Venue
                {
                    Id = 2,
                    Phone = "1234567891",
                    Address = "Address of Venue 2.",
                    Description = "Venue 2.",
                },
            };

            venueRepositoryMock.Setup(mock => mock.GetAll()).Returns(venues.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var venueService = new VenueService(venueRepositoryMock.Object, exceptionHelperMock.Object);

            var venue = new Venue
            {
                Id = 1,
                Phone = "1234567891",
                Address = "Address of Venue 2.",
                Description = "Venue 3.",
            };

            // Act
            Func<Task> action = async () => await venueService.UpdateAsync(id, venue);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }
    }
}