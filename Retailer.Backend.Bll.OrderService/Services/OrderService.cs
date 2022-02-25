using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Bll.StateMachines;
using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;
using Retailer.Backend.Core.Models.DTO;
using Retailer.Backend.Core.Models.Enums;

using System.Threading.Tasks;

using DTO = Retailer.Backend.Core.Models.DTO;

namespace Retailer.Backend.Bll.OrderService.Services
{
    public class OrderService : IDbOrderService
    {
        private readonly OrderEntityStateMachineFactory _orderEntityStateMachineFactory;
        private readonly IMapper _mapper;

        public IRetailerOrderDbContext DbContext { get; set; }

        public OrderService(
            IRetailerOrderDbContext dbContext,
            OrderEntityStateMachineFactory orderEntityStateMachineFactory,
            IMapper mapper)
        {
            DbContext = dbContext;
            _orderEntityStateMachineFactory = orderEntityStateMachineFactory;
            _mapper = mapper;
        }

        public async Task<DTO.OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order == null) return null;
            return _mapper.Map<DTO.OrderDto>(order);
        }

        public async Task<DTO.OrderDto> CreateOrderAsync([FromBody] OrderRequestDto orderRequest)
        {
            var order = _mapper.Map<Order>(orderRequest);
            order = (await DbContext.Orders.AddAsync(order)).Entity;
            await GetStateMachine(order).FireAsync(OrderStateTrigger.Creating);
            await SaveChangesAsync(); // // it assigns value to order.Id 
            return _mapper.Map<DTO.OrderDto>(order);
        }

        public async Task<DTO.OrderDto> BillOrderAsync(int id, int invoiceId)
        {
            var order = await GetByIdAsync(id);
            if (order == null) return null;
            await GetStateMachine(order).FireAsync(OrderStateTrigger.Billing);
            order.InvoiceId = invoiceId;
            return _mapper.Map<DTO.OrderDto>(order);
        }

        public async Task<DTO.OrderDto> PayOrderAsync(int invoiceId)
        {
            var order = await GetByInvoiceIdAsync(invoiceId);
            if (order == null) return null;
            await GetStateMachine(order).FireAsync(OrderStateTrigger.Paying);
            return _mapper.Map<DTO.OrderDto>(order);
        }

        public async Task<DTO.OrderDto> CloseOrderAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order == null) return null;
            await GetStateMachine(order).FireAsync(OrderStateTrigger.Closing);
            return _mapper.Map<DTO.OrderDto>(order);
        }

        public async Task<int> SaveChangesAsync() => await DbContext.SaveChangesAsync();

        private async Task<Order> GetByIdAsync(int id)
        {
            return await DbContext
                .Orders
                // .Include(o => o.Invoice) // <-- do not use here the Include because the invoice will be unattachable after
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        private async Task<Order> GetByInvoiceIdAsync(int invoiceId)
        {
            return await DbContext
                .Orders
                // .Include(o => o.Invoice)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.InvoiceId == invoiceId);
        }

        protected OrderEntityStateMachine GetStateMachine(Order order)
        {
            return _orderEntityStateMachineFactory.Create(order, (DbContext)DbContext);
        }
    }
}
