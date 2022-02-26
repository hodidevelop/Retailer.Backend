using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DTO;
using Retailer.Backend.Core.Models.Enums;

using System.Threading.Tasks;

namespace Retailer.Backend.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDbOrderService _orderService;
        private readonly IInvoiceService _invoiceService;

        public OrdersController(
            IDbOrderService orderService,
            IInvoiceService invoiceService)
        {
            _orderService = orderService;
            _invoiceService = invoiceService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int id)
        {
            var orderDto = await _orderService.GetOrderByIdAsync(id);
            if (orderDto == null) return NotFound();
            if (orderDto.InvoiceId.HasValue)
            {
                var invoiceDto = await _invoiceService.GetInvoiceByIdAsync(orderDto.InvoiceId.Value);
                orderDto.Invoice = invoiceDto;
            }
            return orderDto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrderDto>> CreateOrderAsync([FromBody] OrderRequestDto orderRequest)
        {
            var orderDto = await _orderService.CreateOrderAsync(orderRequest);
            return CreatedAtAction("GetOrderById", new { id = orderDto.Id }, orderDto);
        }

        [HttpPost("{id}/{invoiceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> BillOrderAsync(int id, int invoiceId)
        {
            var orderDto = await _orderService.BillOrderAsync(id, invoiceId);
            if (orderDto == null) return NotFound($"OrderId={id} not found.");
            var invoiceDto = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            if (invoiceDto == null) return NotFound($"InvoiceId={invoiceId} not found.");
            orderDto.Invoice = invoiceDto;
            await _orderService.SaveChangesAsync();
            return orderDto;
        }

        [HttpPost("{invoiceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> PayOrderAsync(int invoiceId)
        {
            var orderDto = await _orderService.PayOrderAsync(invoiceId);
            if (orderDto == null) return NotFound($"No order found by InvoiceId={invoiceId}.");
            await _orderService.SaveChangesAsync();
            return orderDto;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> CloseOrderAsync(int id)
        {
            var orderDto = await _orderService.CloseOrderAsync(id);
            if (orderDto == null) return NotFound();
            if (orderDto.InvoiceId.HasValue)
            {
                var invoiceDto = await _invoiceService.GetInvoiceByIdAsync(orderDto.InvoiceId.Value);
                if (invoiceDto != null && invoiceDto.State != InvoiceState.Closed)
                    await _invoiceService.CloseInvoiceAsync(orderDto.InvoiceId.Value);
                orderDto.Invoice = invoiceDto;
            }
            await _orderService.SaveChangesAsync();
            return orderDto;
        }
    }
}
