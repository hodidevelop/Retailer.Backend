namespace Retailer.Backend.Core.Abstractions
{
    public interface IOrderRequest
    {
        string CustomerName { get; set; }
        string DeliveryAddress { get; set; }
        string ProductName { get; set; }
        double UnitPrice { get; set; }
        double Quantity { get; set; }
    }
}
