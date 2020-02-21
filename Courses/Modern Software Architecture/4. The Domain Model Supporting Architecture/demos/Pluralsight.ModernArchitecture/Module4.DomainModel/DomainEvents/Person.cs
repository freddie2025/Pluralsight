using System;
using Module4.DomainModel.DomainEvents.Events;

namespace Module4.DomainModel.DomainEvents
{
    public class Person : Party
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        
        protected Person()
        {

        }

        public void Apply(PersonRegisteredEvent evt)
        {
            Id = evt.PersonId;
            FirstName = evt.FirstName;
            LastName = evt.LastName;
        }

        public static class Factory
        {
            public static Person CreateNewEntry(string firstName, string lastName, DateTime? dateOfBirth)
            {
                var e = new PersonRegisteredEvent(Guid.NewGuid(), firstName, lastName);
                var p = new Person();
                p.RaiseEvent(e);
                return p;
            }
        }
    }
}

