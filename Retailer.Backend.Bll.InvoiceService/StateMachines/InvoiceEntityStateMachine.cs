using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;
using Retailer.Backend.Core.Models.Enums;
using Retailer.Backend.Core.StateMachines;

namespace Retailer.Backend.Bll.InvoiceService.StateMachines
{
    public class InvoiceEntityStateMachine : EntityStateMachine<Invoice, InvoiceState, InvoiceStateTrigger>
    {
        public InvoiceEntityStateMachine(Invoice invoice, DbContext dbContext, ITimeService timeService)
            : base(invoice, () => invoice.State, s => invoice.State = s, dbContext, timeService)
        {
            ConfigureStateMachine();

            OnTransitioned(t => {
                if (t.Trigger == InvoiceStateTrigger.Paying)
                    invoice.AmountPaid += (double)t.Parameters[0];
            });
        }

        private void ConfigureStateMachine()
        {
            Configure(InvoiceState.None)
                .Permit(InvoiceStateTrigger.Creating, InvoiceState.New);

            Configure(InvoiceState.New)
                .Permit(InvoiceStateTrigger.Paying, InvoiceState.Paid)
                .Permit(InvoiceStateTrigger.Closing, InvoiceState.Closed);

            Configure(InvoiceState.Paid)
                .PermitReentry(InvoiceStateTrigger.Paying)
                .Permit(InvoiceStateTrigger.Closing, InvoiceState.Closed);
        }
    }
}
