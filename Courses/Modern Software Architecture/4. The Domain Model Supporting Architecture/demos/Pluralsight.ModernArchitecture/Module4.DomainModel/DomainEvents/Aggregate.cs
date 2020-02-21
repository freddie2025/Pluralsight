using System;
using System.Collections.Generic;
using System.Linq;

namespace Module4.DomainModel.DomainEvents
{
    public abstract class Aggregate : IAggregate
    {
        public Guid Id { get; protected set; }

        private readonly IList<DomainEvent> _uncommittedEvents = new List<DomainEvent>();

        Guid IAggregate.Id
        {
            get
            {
                return Id;
            }
        }

        bool IAggregate.HasPendingChanges
        {
            get
            {
                return _uncommittedEvents.Any();
            }
        }

        IEnumerable<DomainEvent> IAggregate.GetUncommittedEvents()
        {
            return _uncommittedEvents.ToArray();
        }

        void IAggregate.ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void RaiseEvent(DomainEvent @event, bool fireAndForget = false)
        {
            // This is firing off the event synchronously
            if (fireAndForget)
            {
                (this as dynamic).Apply((dynamic) @event);
                return;
            }
            
            _uncommittedEvents.Add(@event);
        }
    }
}
