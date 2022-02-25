using Microsoft.AspNetCore.Mvc;

using Retailer.Backend.Core.Models.DTO;

using System.Threading.Tasks;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderByIdAsync(int id);

        Task<OrderDto> CreateOrderAsync([FromBody] OrderRequestDto orderRequest);

        Task<OrderDto> BillOrderAsync(int id, int invoiceId);

        Task<OrderDto> PayOrderAsync(int invoiceId);

        Task<OrderDto> CloseOrderAsync(int id);
    }
}
