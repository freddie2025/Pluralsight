using System;
using System.Collections.Generic;

namespace Module4.DomainModel.Aggregates
{
    public class Company : Party
    {
        public string CompanyName { get; private set; }
        protected IList<Address> AddressList { get; private set; }
        protected Company()
        {
            Id = Guid.NewGuid();
            AddressList = new List<Address>();
        }

        public Company AddAddress(Address address)
        {
            AddressList.Add(address);
            return this;
        }

        public static class Factory
        {
            public static Company CreateNew(string companyName, string vatIndex)
            {
                if (String.IsNullOrWhiteSpace(companyName))
                    throw new ArgumentException("Can't be null or empty", "companyName");
                if (String.IsNullOrWhiteSpace(vatIndex))
                    throw new ArgumentException("Can't be null or empty", "vatIndex");

                var company = new Company { CompanyName = companyName, VatIndex = vatIndex };
                return company;
            }
        }
    }
}
