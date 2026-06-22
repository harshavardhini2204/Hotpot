using HotpotWebApplication.Data;
using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HotpotWebApplication.Tests.Services
{
    [TestFixture]
    public class CartServiceTest
    {
        private Mock<ICartRepository> _repoMock;
        private ApplicationDbContext _context;
        private CartService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<ICartRepository>();

            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            _service = new CartService(
                _repoMock.Object,
                _context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task When_GetCartByUserId_ExistingUser_ReturnsCart()
        {
            var cart = new Cart
            {
                CartId = 1,
                UserId = 1
            };

            _repoMock
                .Setup(x => x.GetCartByUserIdAsync(1))
                .ReturnsAsync(cart);

            var result =
                await _service.GetCartByUserIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.UserId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetCartByUserId_InvalidUser_ReturnsNull()
        {
            _repoMock
                .Setup(x => x.GetCartByUserIdAsync(999))
                .ReturnsAsync((Cart?)null);

            var result =
                await _service.GetCartByUserIdAsync(999);

            Assert.That(result, Is.Null);
        }

        [TestCase(1, 5, true)]
        [TestCase(999, 5, false)]
        public async Task When_UpdateQuantity_CartItemProvided_ReturnsExpectedResult(
            int cartItemId,
            int quantity,
            bool expectedResult)
        {
            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetCartItemByIdAsync(cartItemId))
                    .ReturnsAsync(new CartItem
                    {
                        CartItemId = cartItemId,
                        Quantity = 1
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetCartItemByIdAsync(cartItemId))
                    .ReturnsAsync((CartItem?)null);
            }

            var result =
                await _service.UpdateQuantityAsync(
                    cartItemId,
                    quantity);

            Assert.That(result,
                Is.EqualTo(expectedResult));
        }

        [TestCase(1, true)]
        [TestCase(999, false)]
        public async Task When_RemoveCartItem_IdProvided_ReturnsExpectedResult(
            int cartItemId,
            bool expectedResult)
        {
            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetCartItemByIdAsync(cartItemId))
                    .ReturnsAsync(new CartItem
                    {
                        CartItemId = cartItemId
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetCartItemByIdAsync(cartItemId))
                    .ReturnsAsync((CartItem?)null);
            }

            var result =
                await _service.RemoveCartItemAsync(cartItemId);

            Assert.That(result,
                Is.EqualTo(expectedResult));
        }
    }
}