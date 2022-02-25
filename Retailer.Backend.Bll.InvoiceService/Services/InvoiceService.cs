using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;
using Retailer.Backend.Core.Models.Enums;
using Retailer.Backend.Bll.InvoiceService.StateMachines;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Retailer.Backend.Core.Models.DTO;

using DTO = Retailer.Backend.Core.Models.DTO;

namespace Retailer.Backend.Bll.InvoiceService.Services
{
    public class InvoiceService : IDbInvoiceService
    {
        private readonly InvoiceEntityStateMachineFactory _invoiceEntityStateMachineFactory;
        private readonly IMapper _mapper;

        public IRetailerInvoiceDbContext DbContext { get; set; }

        public InvoiceService(
            IRetailerInvoiceDbContext dbContext,
            InvoiceEntityStateMachineFactory invoiceEntityStateMachineFactory,
            IMapper mapper)
        {
            DbContext = dbContext;
            _invoiceEntityStateMachineFactory = invoiceEntityStateMachineFactory;
            _mapper = mapper;
        }

        public async Task<DTO.InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await GetByIdAsync(id);
            if (invoice == null) return null;
            return _mapper.Map<DTO.InvoiceDto>(invoice);
        }

        public async Task<DTO.InvoiceDto> CreateInvoiceAsync([FromBody] InvoiceRequestDto invoiceRequest)
        {
            var invoice = new Invoice
            {
                BillingAddress = invoiceRequest.BillingAddress,
                Amount = invoiceRequest.Amount
            };
            invoice = (await DbContext.Invoices.AddAsync(invoice)).Entity;
            await GetStateMachine(invoice).FireAsync(InvoiceStateTrigger.Creating);
            await SaveChangesAsync(); // it assigns value to invoice.Id 
            return _mapper.Map<DTO.InvoiceDto>(invoice);
        }

        public async Task<DTO.InvoiceDto> PayInvoiceAsync(int id, double amount)
        {
            var invoice = await GetByIdAsync(id);
            if (invoice == null) return null;
            if (amount != 0)
            {
                // invoice state set to Paid (pass currently paid amount to state-machine as parameter)
                var invoiceStateMachine = GetStateMachine(invoice);
                var invoiceStateTriggerPaying = invoiceStateMachine.SetTriggerParameters<double>(InvoiceStateTrigger.Paying);
                await invoiceStateMachine.FireAsync(invoiceStateTriggerPaying, amount);
                if (invoice.AmountPaid >= invoice.Amount)
                    await invoiceStateMachine.FireAsync(InvoiceStateTrigger.Closing);
            }
            return _mapper.Map<DTO.InvoiceDto>(invoice);
        }

        public async Task<DTO.InvoiceDto> CloseInvoiceAsync(int id)
        {
            var invoice = await GetByIdAsync(id);
            if (invoice == null) return null;
            await GetStateMachine(invoice).FireAsync(InvoiceStateTrigger.Closing);
            return _mapper.Map<DTO.InvoiceDto>(invoice);
        }

        public async Task<int> SaveChangesAsync() => await DbContext.SaveChangesAsync();

        private async Task<Invoice> GetByIdAsync(int id)
        {
            var invoice = DbContext
                .Invoices
                .AsNoTracking()
                .FirstOrDefault(i => i.Id == id);
            return await Task.FromResult(invoice);
        }

        protected InvoiceEntityStateMachine GetStateMachine(Invoice invoice)
        {
            return _invoiceEntityStateMachineFactory.Create(invoice, (DbContext)DbContext);
        }
    }
}
