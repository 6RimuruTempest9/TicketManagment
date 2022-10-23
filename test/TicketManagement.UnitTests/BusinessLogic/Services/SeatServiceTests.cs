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
    public class SeatServiceTests
    {
        [Test]
        public void DeleteAsync_WhenInputIdOfNonExistSeat_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await seatService.DeleteAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void GetByIdAsync_WhenInputIdOfNonExistSeat_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await seatService.GetByIdAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void InsertAsync_WhenInputSeatIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await seatService.InsertAsync(null);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void InsertAsync_WhenInputSeatHasAlreadyExistRowAndNumberValuesInTheSameArea_ShouldThrowModelException()
        {
            // Arrange
            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            var seat = new Seat
            {
                Id = 3,
                AreaId = 1,
                Row = 1,
                Number = 1,
            };

            // Act
            Func<Task> action = async () => await seatService.InsertAsync(seat);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }

        [Test]
        public void UpdateAsync_WhenInputIdOfNonExistSeat_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            var seat = new Seat
            {
                Id = 1,
                AreaId = 2,
                Row = 1,
                Number = 2,
            };

            // Act
            Func<Task> action = async () => await seatService.UpdateAsync(id, seat);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void UpdateAsync_WhenInputSeatIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var id = 2;

            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            var seat = default(Seat);

            // Act
            Func<Task> action = async () => await seatService.UpdateAsync(id, seat);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void UpdateAsync_WhenInputSeatHasAlreadyExistRowAndNumberValuesInTheSameArea_ShouldThrowModelException()
        {
            // Arrange
            var id = 2;

            var seatRepositoryMock = new Mock<IAsyncRepository<Seat>>();

            var seats = new[]
            {
                new Seat
                {
                    Id = 1,
                    AreaId = 1,
                    Row = 1,
                    Number = 1,
                },
                new Seat
                {
                    Id = 2,
                    AreaId = 2,
                    Row = 1,
                    Number = 1,
                },
            };

            seatRepositoryMock.Setup(mock => mock.GetAll()).Returns(seats.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var seatService = new SeatService(seatRepositoryMock.Object, exceptionHelperMock.Object);

            var seat = new Seat
            {
                Id = 2,
                AreaId = 1,
                Row = 1,
                Number = 1,
            };

            // Act
            Func<Task> action = async () => await seatService.UpdateAsync(id, seat);

            // Assert
            action.Should().ThrowAsync<ModelException>();
        }
    }
}