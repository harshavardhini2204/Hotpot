using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implementations;
using Moq;

namespace HotpotWebApplication.Tests.Services
{
    [TestFixture]
    public class MenuItemServiceTest
    {
        private Mock<IMenuItemRepository> _repoMock;
        private MenuItemService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IMenuItemRepository>();
            _service = new MenuItemService(_repoMock.Object);
        }

        [Test]
        public async Task When_GetMenuItemById_ExistingId_ReturnsMenuItem()
        {
            // Arrange
            var item = new MenuItem
            {
                MenuItemId = 1,
                ItemName = "Chicken Burger",
                Price = 150
            };

            _repoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(item);

            // Act
            var result =
                await _service.GetMenuItemByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ItemName,
                Is.EqualTo("Chicken Burger"));
        }

        [Test]
        public async Task When_GetMenuItemById_InvalidId_ReturnsNull()
        {
            // Arrange
            _repoMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((MenuItem?)null);

            // Act
            var result =
                await _service.GetMenuItemByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [TestCase("Burger", 150)]
        [TestCase("Pizza", 250)]
        [TestCase("Biryani", 300)]
        public async Task When_CreateMenuItem_ValidData_SavesMenuItem(
            string itemName,
            decimal price)
        {
            // Arrange
            var item = new MenuItem
            {
                ItemName = itemName,
                Price = price
            };

            // Act
            await _service.CreateMenuItemAsync(item);

            // Assert
            _repoMock.Verify(
                x => x.AddAsync(It.Is<MenuItem>(
                    m => m.ItemName == itemName &&
                         m.Price == price)),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [Test]
        public async Task When_UpdateMenuItem_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var item = new MenuItem
            {
                MenuItemId = 1,
                ItemName = "Updated Burger",
                Price = 200
            };

            // Act
            await _service.UpdateMenuItemAsync(item);

            // Assert
            _repoMock.Verify(
                x => x.UpdateAsync(It.IsAny<MenuItem>()),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [TestCase(1, true)]
        [TestCase(999, false)]
        public async Task When_DeleteMenuItem_IdProvided_ReturnsExpectedResult(
            int menuItemId,
            bool expectedResult)
        {
            // Arrange

            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(menuItemId))
                    .ReturnsAsync(new MenuItem
                    {
                        MenuItemId = menuItemId,
                        ItemName = "Burger"
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(menuItemId))
                    .ReturnsAsync((MenuItem?)null);
            }

            // Act
            var result =
                await _service.DeleteMenuItemAsync(menuItemId);

            // Assert
            Assert.That(result,
                Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task When_GetAllMenuItems_DataExists_ReturnsMenuItems()
        {
            // Arrange
            var items = new List<MenuItem>
            {
                new()
                {
                    MenuItemId = 1,
                    ItemName = "Burger"
                },
                new()
                {
                    MenuItemId = 2,
                    ItemName = "Pizza"
                }
            };

            _repoMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(items);

            // Act
            var result =
                await _service.GetAllMenuItemsAsync();

            // Assert
            Assert.That(result.Count(),
                Is.EqualTo(2));
        }
    }
}