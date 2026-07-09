using HotpotWebApplication.Data;
using HotpotWebApplication.DTOs.Payment;
using HotpotWebApplication.Models;
using HotpotWebApplication.Models.Enums;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotpotWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(IOrderService orderService,IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult>GetPayments()
        {
            var payments =
               await _paymentService.GetAllPaymentsAsync();

            return Ok(payments);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetPaymentById(int id)
        {
            var payment =
                await _paymentService.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
          
        {
            var order = await _orderService.GetOrderByIdAsync(dto.OrderId);

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = dto.PaymentMethod == "Cash on Delivery"
        ? PaymentStatus.Pending
        : PaymentStatus.Completed,
                Amount = order.TotalAmount,
                TransactionReference = Guid.NewGuid().ToString("N")[..10].ToUpper(),
                PaymentDate = DateTime.Now
            };

            var createdPayment =
                await _paymentService.CreatePaymentAsync(payment);

            return Ok(createdPayment);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdatePayment(int id, PaymentStatus status)
        {

            var payment =
        await _paymentService.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound();

            payment.PaymentStatus = status;

            await _paymentService.UpdatePaymentAsync(payment);

            return Ok(new
            {
                Message = "Payment status updated successfully",
                PaymentId = payment.PaymentId,
                Status = payment.PaymentStatus
            });


        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeletePayment(int id)
        {
            var deleted =
                await _paymentService.DeletePaymentAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Payment deleted successfully");

        }

       
    }
}
