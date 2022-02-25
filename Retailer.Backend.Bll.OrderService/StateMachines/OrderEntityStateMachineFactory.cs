using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;

namespace Retailer.Backend.Bll.StateMachines
{
    public class OrderEntityStateMachineFactory
    {
        private readonly ITimeService _timeService;

        public OrderEntityStateMachineFactory(
            ITimeService timeService)
        {
            _timeService = timeService;
        }

        public OrderEntityStateMachine Create(Order order, DbContext dbContext)
        {
            return new OrderEntityStateMachine(order, dbContext, _timeService);
        }
    }
}
