using Retailer.Backend.Core.Abstractions;

namespace Retailer.Backend.Core.Models.DTO
{
    public class OrderRequestDto : IOrderRequest
    {
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
    }
}
