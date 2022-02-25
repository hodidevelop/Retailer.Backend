namespace Retailer.Backend.Core.Abstractions
{
    public interface IInvoiceRequest
    {
        int OrderId { get; set; }

        string BillingAddress { get; set; }

        double Amount { get; set; }
    }
}
