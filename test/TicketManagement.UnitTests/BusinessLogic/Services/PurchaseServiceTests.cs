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
    public class PurchaseServiceTests
    {
        [Test]
        public void DeleteAsync_WhenInputIdOfNonExistPurchase_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var purchaseRepositoryMock = new Mock<IAsyncRepository<Purchase>>();

            var purchases = new[]
            {
                new Purchase
                {
                    Id = 1,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
                new Purchase
                {
                    Id = 2,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
            };

            purchaseRepositoryMock.Setup(mock => mock.GetAll()).Returns(purchases.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var purchaseService = new PurchaseService(purchaseRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await purchaseService.DeleteAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void GetByIdAsync_WhenInputIdOfNonExistPurchase_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var purchaseRepositoryMock = new Mock<IAsyncRepository<Purchase>>();

            var purchases = new[]
            {
                new Purchase
                {
                    Id = 1,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
                new Purchase
                {
                    Id = 2,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
            };

            purchaseRepositoryMock.Setup(mock => mock.GetAll()).Returns(purchases.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var purchaseService = new PurchaseService(purchaseRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await purchaseService.GetByIdAsync(id);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void InsertAsync_WhenInputPurchaseIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var purchaseRepositoryMock = new Mock<IAsyncRepository<Purchase>>();

            var purchases = new[]
            {
                new Purchase
                {
                    Id = 1,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
                new Purchase
                {
                    Id = 2,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
            };

            purchaseRepositoryMock.Setup(mock => mock.GetAll()).Returns(purchases.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var purchaseService = new PurchaseService(purchaseRepositoryMock.Object, exceptionHelperMock.Object);

            // Act
            Func<Task> action = async () => await purchaseService.InsertAsync(null);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public void UpdateAsync_WhenInputIdOfNonExistPurchase_ShouldThrowModelNotFoundException()
        {
            // Arrange
            var id = -1;

            var purchaseRepositoryMock = new Mock<IAsyncRepository<Purchase>>();

            var purchases = new[]
            {
                new Purchase
                {
                    Id = 1,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
                new Purchase
                {
                    Id = 2,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
            };

            purchaseRepositoryMock.Setup(mock => mock.GetAll()).Returns(purchases.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var purchaseService = new PurchaseService(purchaseRepositoryMock.Object, exceptionHelperMock.Object);

            var purchase = new Purchase
            {
                Id = 1,
                UserId = "User 2",
                EventSeatId = 1,
                Time = new DateTime(2022, 2, 5, 23, 0, 0),
            };

            // Act
            Func<Task> action = async () => await purchaseService.UpdateAsync(id, purchase);

            // Assert
            action.Should().ThrowAsync<ModelNotFoundException>();
        }

        [Test]
        public void UpdateAsync_WhenInputPurchaseIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var id = 2;

            var purchaseRepositoryMock = new Mock<IAsyncRepository<Purchase>>();

            var purchases = new[]
            {
                new Purchase
                {
                    Id = 1,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
                new Purchase
                {
                    Id = 2,
                    UserId = "User 1",
                    EventSeatId = 1,
                    Time = new DateTime(2022, 2, 5, 22, 0, 0),
                },
            };

            purchaseRepositoryMock.Setup(mock => mock.GetAll()).Returns(purchases.AsQueryable());

            var exceptionHelperMock = new Mock<ExceptionHelper>();

            var purchaseService = new PurchaseService(purchaseRepositoryMock.Object, exceptionHelperMock.Object);

            var purchase = default(Purchase);

            // Act
            Func<Task> action = async () => await purchaseService.UpdateAsync(id, purchase);

            // Assert
            action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}