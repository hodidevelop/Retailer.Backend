using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;

using Stateless;

using System;

namespace Retailer.Backend.Core.StateMachines
{
    public abstract class EntityStateMachine<TEntity, TState, TTrigger> : StateMachine<TState, TTrigger>
        where TEntity : class, IIdentifiedEntity, IAuditModifiedDateEntity
        where TState : struct, Enum
        where TTrigger : Enum
    {

        protected EntityStateMachine(
            TEntity entity,
            Func<TState> stateSelector,
            Action<TState> stateMutator,
            DbContext dbContext,
            ITimeService timeService,
            string exceptionMessage = null)
            : base(stateSelector, stateMutator)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
                dbContext.Attach(entity);

            OnTransitionCompleted(t => entity.ModifiedDate = timeService.Now);

            OnUnhandledTrigger((s, t) =>
            {
                throw new Exceptions.InvalidStateTransitionException(
                    exceptionMessage ?? $"The requested state-transition is not enabled! (state: {s} trigger: {t})");
            });
        }
    }
}
