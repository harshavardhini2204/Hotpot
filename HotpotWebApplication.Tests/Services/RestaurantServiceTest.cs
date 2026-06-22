using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implementations;
using Moq;

namespace HotpotWebApplication.Tests.Services
{
    [TestFixture]
    public class RestaurantServiceTest
    {
        private Mock<IRestaurantRepository> _repoMock;
        private RestaurantService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRestaurantRepository>();
            _service = new RestaurantService(_repoMock.Object);
        }

        [Test]
        public async Task When_GetRestaurantById_ExistingId_ReturnsRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                RestaurantId = 1,
                RestaurantName = "Food Hub"
            };

            _repoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(restaurant);

            // Act
            var result =
                await _service.GetRestaurantByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.RestaurantName,
                Is.EqualTo("Food Hub"));
        }

        [Test]
        public async Task When_GetRestaurantById_InvalidId_ReturnsNull()
        {
            // Arrange
            _repoMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Restaurant?)null);

            // Act
            var result =
                await _service.GetRestaurantByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [TestCase("Food Hub")]
        [TestCase("Spice Garden")]
        [TestCase("Burger Point")]
        public async Task When_CreateRestaurant_ValidData_SavesRestaurant(
            string restaurantName)
        {
            // Arrange
            var restaurant = new Restaurant
            {
                RestaurantName = restaurantName
            };

            // Act
            await _service.CreateRestaurantAsync(restaurant);

            // Assert
            _repoMock.Verify(
                x => x.AddAsync(It.Is<Restaurant>(
                    r => r.RestaurantName == restaurantName)),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [Test]
        public async Task When_UpdateRestaurant_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                RestaurantId = 1,
                RestaurantName = "Updated Restaurant"
            };

            // Act
            await _service.UpdateRestaurantAsync(restaurant);

            // Assert
            _repoMock.Verify(
                x => x.UpdateAsync(It.IsAny<Restaurant>()),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [TestCase(1, true)]
        [TestCase(999, false)]
        public async Task When_DeleteRestaurant_IdProvided_ReturnsExpectedResult(
            int restaurantId,
            bool expectedResult)
        {
            // Arrange

            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(restaurantId))
                    .ReturnsAsync(new Restaurant
                    {
                        RestaurantId = restaurantId,
                        RestaurantName = "Food Hub"
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(restaurantId))
                    .ReturnsAsync((Restaurant?)null);
            }

            // Act
            var result =
                await _service.DeleteRestaurantAsync(restaurantId);

            // Assert
            Assert.That(result,
                Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task When_GetAllRestaurants_DataExists_ReturnsRestaurants()
        {
            // Arrange
            var restaurants = new List<Restaurant>
            {
                new()
                {
                    RestaurantId = 1,
                    RestaurantName = "Food Hub"
                },
                new()
                {
                    RestaurantId = 2,
                    RestaurantName = "Burger Point"
                }
            };

            _repoMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(restaurants);

            // Act
            var result =
                await _service.GetAllRestaurantsAsync();

            // Assert
            Assert.That(result.Count(),
                Is.EqualTo(2));
        }
    }
}