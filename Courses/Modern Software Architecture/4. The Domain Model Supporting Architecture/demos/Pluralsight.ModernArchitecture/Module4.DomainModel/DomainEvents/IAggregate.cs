using System;
using System.Collections.Generic;

namespace Module4.DomainModel.DomainEvents
{
    public interface IAggregate
    {
        Guid Id { get; }

        bool HasPendingChanges { get; }

        IEnumerable<DomainEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}
