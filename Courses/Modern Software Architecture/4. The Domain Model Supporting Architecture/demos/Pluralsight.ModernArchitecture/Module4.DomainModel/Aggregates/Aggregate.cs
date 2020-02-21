using System;

namespace Module4.DomainModel.Aggregates
{
    public abstract class Aggregate : IAggregate
    {
        public Guid Id { get; protected set; }

        Guid IAggregate.Id
        {
            get
            {
                return Id;
            }
        }
    }
}
