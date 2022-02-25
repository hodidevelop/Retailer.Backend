using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.Enums;

namespace Retailer.Backend.Core.Models.DTO
{
    // no InvoiceDto references here
    public class OrderDto : IOrderRequest
    {
        public int Id { get; set; }

        public OrderState State { get; set; } = OrderState.None;

        public string CustomerName { get; set; }

        public string DeliveryAddress { get; set; }

        public string ProductName { get; set; }

        public double UnitPrice { get; set; }

        public double Quantity { get; set; }

        public int? InvoiceId { get; set; }

        public InvoiceDto Invoice { get; set; }
    }
}