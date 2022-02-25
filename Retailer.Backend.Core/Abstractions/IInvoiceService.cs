using Microsoft.AspNetCore.Mvc;

using Retailer.Backend.Core.Models.DTO;

using System.Threading.Tasks;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);

        Task<InvoiceDto> CreateInvoiceAsync([FromBody] InvoiceRequestDto invoiceRequest);

        Task<InvoiceDto> PayInvoiceAsync(int id, double amount);

        Task<InvoiceDto> CloseInvoiceAsync(int id);
    }
}
