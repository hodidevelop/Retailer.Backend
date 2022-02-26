using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Extensions;
using Retailer.Backend.Core.Models.DTO;
using Retailer.Backend.Core.Models.Enums;

using System.Threading.Tasks;

namespace Retailer.Backend.InvoiceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IDbInvoiceService _invoiceService;
        private readonly IOrderService _orderService;

        public InvoicesController(
            IDbInvoiceService invoiceService,
            IOrderService orderService)
        {
            _invoiceService = invoiceService;
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceByIdAsync(int id)
        {
            var invoiceDto = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoiceDto == null) return NotFound();
            return invoiceDto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceDto>> CreateInvoiceAsync([FromBody] InvoiceRequestDto invoiceRequest)
        {
            if (invoiceRequest.OrderId == 0)
                return BadRequest("OrderId field in invoice request cannot be zero.");
            var orderDto = await _orderService.GetOrderByIdAsync(invoiceRequest.OrderId);
            if (orderDto == null) return NotFound();
            invoiceRequest.SetOrderData((OrderDto)orderDto);
            var invoiceDto = await _invoiceService.CreateInvoiceAsync(invoiceRequest);
            await _orderService.BillOrderAsync(invoiceRequest.OrderId, invoiceDto.Id);
            return CreatedAtAction("GetInvoiceById", new { id = invoiceDto.Id }, invoiceDto);
        }

        [HttpPut("{id}/{amount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceDto>> PayInvoiceAsync(int id, double amount)
        {
            var invoiceDto = await _invoiceService.PayInvoiceAsync(id, amount);
            if (invoiceDto == null) return NotFound();
            if (invoiceDto.State == InvoiceState.Closed)
                await _orderService.PayOrderAsync(invoiceDto.Id);
            await _invoiceService.SaveChangesAsync();
            return invoiceDto;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceDto>> CloseInvoiceAsync(int id)
        {
            var invoiceDto = await _invoiceService.CloseInvoiceAsync(id);
            await _invoiceService.SaveChangesAsync();
            return invoiceDto;
        }
    }
}
