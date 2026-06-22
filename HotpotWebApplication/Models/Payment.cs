using System.Text.Json.Serialization;
using HotpotWebApplication.Models.Enums;

namespace HotpotWebApplication.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod {  get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public  PaymentStatus PaymentStatus {  get; set; }
        public string? TransactionReference {  get; set; }
        public DateTime PaymentDate { get; set; }
        public Order? Order { get; set; }
    }
}
