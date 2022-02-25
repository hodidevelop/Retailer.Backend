using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;
using Retailer.Backend.Core.Models.Enums;
using Retailer.Backend.Core.StateMachines;

namespace Retailer.Backend.Bll.StateMachines
{
    public class OrderEntityStateMachine : EntityStateMachine<Order, OrderState, OrderStateTrigger>
    {
        public OrderEntityStateMachine(Order order, DbContext dbContext, ITimeService timeService)
            : base(order, () => order.State, s => order.State = s, dbContext, timeService)
        {
            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
            Configure(OrderState.None)
                .Permit(OrderStateTrigger.Creating, OrderState.New);

            Configure(OrderState.New)
                .Permit(OrderStateTrigger.Billing, OrderState.Billed)
                .Permit(OrderStateTrigger.Closing, OrderState.Closed);

            Configure(OrderState.Billed)
                .Permit(OrderStateTrigger.Paying, OrderState.Paid)
                .Permit(OrderStateTrigger.Closing, OrderState.Closed);

            Configure(OrderState.Paid)
                .Permit(OrderStateTrigger.Closing, OrderState.Closed);
        }
    }
}
