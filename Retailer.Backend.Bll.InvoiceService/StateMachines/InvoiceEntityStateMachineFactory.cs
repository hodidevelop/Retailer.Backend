using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;

namespace Retailer.Backend.Bll.InvoiceService.StateMachines
{
    public class InvoiceEntityStateMachineFactory
    {
        private readonly ITimeService _timeService;

        public InvoiceEntityStateMachineFactory(
            ITimeService timeService)
        {
            _timeService = timeService;
        }

        public InvoiceEntityStateMachine Create(Invoice invoice, DbContext dbContext)
        {
            return new InvoiceEntityStateMachine(invoice, dbContext, _timeService);
        }
    }
}
