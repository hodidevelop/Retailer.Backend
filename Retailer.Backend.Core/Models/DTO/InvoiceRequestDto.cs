using Retailer.Backend.Core.Abstractions;

namespace Retailer.Backend.Core.Models.DTO
{
    public class InvoiceRequestDto : IInvoiceRequest
    {
        public int OrderId { get; set; }
        public string BillingAddress { get; set; }
        public double Amount { get; set; }
    }
}
