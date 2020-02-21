using System;

namespace Module4.DomainModel.Aggregates
{
    public interface IAggregate 
    {
        Guid Id { get; }
    }
}
