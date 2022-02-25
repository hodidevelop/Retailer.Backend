using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Retailer.Backend.Core.Abstractions;

using System.Net.Http;
using System.Threading.Tasks;

namespace Retailer.Backend.Bll.OrderService.Services
{
    public class InvoiceService : InvoiceServiceClient, IInvoiceService
    {
        private readonly IMapper _mapper;

        public InvoiceService(HttpClient httpClient, IMapper mapper)
            : base(null, httpClient)
        {
            _mapper = mapper;
        }

        public new async Task<Core.Models.DTO.InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            return _mapper.Map<Core.Models.DTO.InvoiceDto>(await base.GetInvoiceByIdAsync(id));
        }

        public async Task<Core.Models.DTO.InvoiceDto> CreateInvoiceAsync([FromBody] Core.Models.DTO.InvoiceRequestDto invoiceRequest)
        {
            var InvoiceRequestDto = _mapper.Map<InvoiceRequestDto>(invoiceRequest);
            return _mapper.Map<Core.Models.DTO.InvoiceDto>(await base.CreateInvoiceAsync(InvoiceRequestDto));
        }

        public new async Task<Core.Models.DTO.InvoiceDto> PayInvoiceAsync(int id, double amount)
        {
            return _mapper.Map<Core.Models.DTO.InvoiceDto>(await base.PayInvoiceAsync(id, amount));
        }

        public new async Task<Core.Models.DTO.InvoiceDto> CloseInvoiceAsync(int id)
        {
            return _mapper.Map<Core.Models.DTO.InvoiceDto>(await base.CloseInvoiceAsync(id));
        }
    }
}
