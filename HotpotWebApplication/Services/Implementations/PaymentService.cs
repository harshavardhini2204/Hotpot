using HotpotWebApplication.Models;
using HotpotWebApplication.Repositories.Interfaces;
using HotpotWebApplication.Services.Interfaces;

namespace HotpotWebApplication.Services.Implementations
{
    public class PaymentService:IPaymentService
    {
        private readonly IPaymentRepository _repository;
        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            await _repository.AddAsync(payment);
            await _repository.SaveAsync();
            return payment;
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            await _repository.UpdateAsync(payment);
            await _repository.SaveAsync();
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var menu = await _repository.GetByIdAsync(id);

            if (menu == null)
                return false;

            await _repository.DeleteAsync(menu);
            await _repository.SaveAsync();

            return true;
        }
    }
}
