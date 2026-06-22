using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Implementations;
using Moq;

namespace HotpotWebApplication.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTest
    {
        private Mock<IPaymentRepository> _repoMock;
        private PaymentService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPaymentRepository>();
            _service = new PaymentService(_repoMock.Object);
        }

        [Test]
        public async Task When_GetPaymentById_ExistingId_ReturnsPayment()
        {
            // Arrange
            var payment = new Payment
            {
                PaymentId = 1,
                OrderId = 1,
                Amount = 500
            };

            _repoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(payment);

            // Act
            var result = await _service.GetPaymentByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.PaymentId, Is.EqualTo(1));
        }

        [Test]
        public async Task When_GetPaymentById_InvalidId_ReturnsNull()
        {
            // Arrange
            _repoMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Payment?)null);

            // Act
            var result = await _service.GetPaymentByIdAsync(999);

            // Assert
            Assert.That(result, Is.Null);
        }

        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(1500)]
        public async Task When_CreatePayment_ValidData_SavesPayment(decimal amount)
        {
            // Arrange
            var payment = new Payment
            {
                OrderId = 1,
                Amount = amount
            };

            // Act
            await _service.CreatePaymentAsync(payment);

            // Assert
            _repoMock.Verify(
                x => x.AddAsync(It.Is<Payment>(
                    p => p.Amount == amount)),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [Test]
        public async Task When_UpdatePayment_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var payment = new Payment
            {
                PaymentId = 1,
                Amount = 1200
            };

            // Act
            await _service.UpdatePaymentAsync(payment);

            // Assert
            _repoMock.Verify(
                x => x.UpdateAsync(It.IsAny<Payment>()),
                Times.Once);

            _repoMock.Verify(
                x => x.SaveAsync(),
                Times.Once);
        }

        [TestCase(1, true)]
        [TestCase(999, false)]
        public async Task When_DeletePayment_IdProvided_ReturnsExpectedResult(
            int paymentId,
            bool expectedResult)
        {
            // Arrange
            if (expectedResult)
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(paymentId))
                    .ReturnsAsync(new Payment
                    {
                        PaymentId = paymentId,
                        Amount = 500
                    });
            }
            else
            {
                _repoMock
                    .Setup(x => x.GetByIdAsync(paymentId))
                    .ReturnsAsync((Payment?)null);
            }

            // Act
            var result =
                await _service.DeletePaymentAsync(paymentId);

            // Assert
            Assert.That(result,
                Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task When_GetAllPayments_DataExists_ReturnsPayments()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new()
                {
                    PaymentId = 1,
                    Amount = 500
                },
                new()
                {
                    PaymentId = 2,
                    Amount = 1000
                }
            };

            _repoMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(payments);

            // Act
            var result =
                await _service.GetAllPaymentsAsync();

            // Assert
            Assert.That(result.Count(),
                Is.EqualTo(2));
        }
    }
}