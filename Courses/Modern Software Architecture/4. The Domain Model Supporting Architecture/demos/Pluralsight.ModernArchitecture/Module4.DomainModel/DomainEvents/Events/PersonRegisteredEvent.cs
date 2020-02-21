using System;

namespace Module4.DomainModel.DomainEvents.Events
{
    public class PersonRegisteredEvent : DomainEvent
    {
        public Guid PersonId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public PersonRegisteredEvent(Guid personId, string firstName, string lastName)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}