using System;
using Module4.DomainModel.DomainEvents.Events;

namespace Module4.DomainModel.DomainEvents
{
    public class Company : Party
    {
        public string CompanyName { get; private set; }
        protected Company()
        {
        }

        public void Apply(CompanyRegisteredEvent evt)
        {
            Id = evt.CompanyId;
            CompanyName = evt.CompanyName;
            VatIndex = evt.VatIndex;           
        }

        public static class Factory
        {
            public static Company CreateNewEntry(string companyName, string vatIndex)
            {
                var p = new Company();
                var e = new CompanyRegisteredEvent(Guid.NewGuid(), companyName, vatIndex);
                p.RaiseEvent(e);
                return p;
            }
        }
    }
}
