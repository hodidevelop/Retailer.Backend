using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Retailer.Backend.Core.Abstractions;

using System.Net.Http;
using System.Threading.Tasks;

namespace Retailer.Backend.Bll.InvoiceService.Services
{
    public class OrderService : OrderServiceClient, IOrderService
    {
        private readonly IMapper _mapper;

        public OrderService(HttpClient httpClient, IMapper mapper)
            : base(null, httpClient)
        {
            _mapper = mapper;
        }

        public new async Task<Core.Models.DTO.OrderDto> GetOrderByIdAsync(int id)
        {
            return _mapper.Map<Core.Models.DTO.OrderDto>(await base.GetOrderByIdAsync(id));
        }

        public async Task<Core.Models.DTO.OrderDto> CreateOrderAsync([FromBody] Core.Models.DTO.OrderRequestDto orderRequest)
        {
            var orderRequestDto = _mapper.Map<OrderRequestDto>(orderRequest);
            return _mapper.Map<Core.Models.DTO.OrderDto>(await base.CreateOrderAsync(orderRequestDto));
        }

        public new async Task<Core.Models.DTO.OrderDto> BillOrderAsync(int id, int invoiceId)
        {
            return _mapper.Map<Core.Models.DTO.OrderDto>(await base.BillOrderAsync(id, invoiceId));
        }

        public new async Task<Core.Models.DTO.OrderDto> PayOrderAsync(int invoiceId)
        {
            return _mapper.Map<Core.Models.DTO.OrderDto>(await base.PayOrderAsync(invoiceId));
        }

        public new async Task<Core.Models.DTO.OrderDto> CloseOrderAsync(int id)
        {
            return _mapper.Map<Core.Models.DTO.OrderDto>(await base.CloseOrderAsync(id));
        }
    }
}
