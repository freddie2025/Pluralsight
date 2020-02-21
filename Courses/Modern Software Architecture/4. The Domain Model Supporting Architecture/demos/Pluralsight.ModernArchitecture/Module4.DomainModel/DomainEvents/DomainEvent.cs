using System;

namespace Module4.DomainModel.DomainEvents
{
    public class DomainEvent : Message
    {
        public DateTime TimeStamp { get; private set; }

        public DomainEvent()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
