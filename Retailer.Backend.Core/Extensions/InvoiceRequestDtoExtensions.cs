using Retailer.Backend.Core.Models.DTO;

namespace Retailer.Backend.Core.Extensions
{
    public static class InvoiceRequestDtoExtensions
    {
        public static void SetOrderData(this InvoiceRequestDto invoiceRequest, OrderDto orderDto)
        {
            if (invoiceRequest.BillingAddress?.Length == 0)
                invoiceRequest.BillingAddress = orderDto.DeliveryAddress;
            if (invoiceRequest.Amount == 0)
                invoiceRequest.Amount = orderDto.UnitPrice * orderDto.Quantity;
        }
    }
}
