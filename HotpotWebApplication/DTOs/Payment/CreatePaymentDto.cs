using System.ComponentModel.DataAnnotations;

namespace HotpotWebApplication.DTOs.Payment
{
    public class CreatePaymentDto
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Range(1,100000)]
        public decimal Amount { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }
        [Required]
        public string? PaymentStatus { get; set; }
        [Required]
        public string? TransactionReference { get; set; }
        [Required]
        public DateTime PaymentDate {  get; set; }
    }
}
