using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implementations;
using Moq;

namespace HotpotWebApplication.Tests.Services
{
    [TestFixture]
    public class OrderServiceTest
    {
        private Mock<IOrderRepository> _repoMock;
        private OrderService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IOrderRepository>();
            _service = new OrderService(_repoMock.Object);
        }

        [Test]
        public async Task When_GetOrderById_ExistingId_ReturnsOrder()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                TotalAmount = 500
            };

            _repoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(order);

            // Act
            var result = await _service.GetOrderByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.OrderId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetOrderById_InvalidId_ReturnsNull()
        {
            // Arrange
            _repoMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Order?)null);

            // Act
            var result = await _service.GetOrderByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(1500)]
        public async Task When_CreateOrder_ValidData_SavesOrder(decimal totalAmount)
        {
            // Arrange
            var order = new Order
            {
                UserId = 1,
                RestaurantId = 1,
                TotalAmount = totalAmount
            };

            // Act
            await _service.CreateOrderAsync(order);

            // Assert
            _repoMock.Verify(
                x => x.AddAsync(It.Is<Order>(
                    o => o.TotalAmount == totalAmount)),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [Test]
        public async Task When_UpdateOrder_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                TotalAmount = 1200
            };

            // Act
            await _service.UpdateOrderAsync(order);

            // Assert
            _repoMock.Verify(
                x => x.UpdateAsync(It.IsAny<Order>()),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [TestCase(1, true)]
        [TestCase(999, false)]
        public async Task When_DeleteOrder_IdProvided_ReturnsExpectedResult(
            int orderId,
            bool expectedResult)
        {
            // Arrange

            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(orderId))
                    .ReturnsAsync(new Order
                    {
                        OrderId = orderId,
                        TotalAmount = 500
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(orderId))
                    .ReturnsAsync((Order?)null);
            }

            // Act
            var result =
                await _service.DeleteOrderAsync(orderId);

            // Assert
            Assert.That(result,
                Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task When_GetAllOrders_DataExists_ReturnsOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new()
                {
                    OrderId = 1,
                    TotalAmount = 500
                },
                new()
                {
                    OrderId = 2,
                    TotalAmount = 1000
                }
            };

            _repoMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(orders);

            // Act
            var result =
                await _service.GetAllOrdersAsync();

            // Assert
            Assert.That(result.Count(),
                Is.EqualTo(2));
        }
    }
}