using System;

namespace Module4.DomainModel.Aggregates
{
    public sealed class Address
    {
        public static Address Create(string street = "", string number = "", string city = "", string zip = "", string country = "")
        {
            var address = new Address { Street = street, Number = number, City = city, Zip = zip, Country = country };
            return address;
        }

        #region Added to please the O/RM

        /// <summary>
        /// Used by the O/RM to materialize objects
        /// </summary>
        private Address()
        {
        }

        #endregion

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string Zip { get; private set; }
        public string Country { get; private set; }

        #region Equality
        public static bool operator ==(Address c1, Address c2)
        {
            // Both null or same instance
            if (ReferenceEquals(c1, c2))
                return true;

            // Return false if one is null, but not both 
            if (((object)c1 == null) || ((object)c2 == null))
                return false;

            return c1.Equals(c2);
        }
        public static bool operator !=(Address c1, Address c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            if (this == (Address)obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Address)obj;
            return string.Equals(Street, other.Street, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(Number, other.Number, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(City, other.City, StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(Zip, other.Zip, StringComparison.InvariantCultureIgnoreCase);
        }
        public override int GetHashCode()
        {
            const int hashIndex = 307;
            var result = (Street != null ? Street.GetHashCode() : 0);
            result = (result * hashIndex) ^ (Number != null ? Number.GetHashCode() : 0);
            result = (result * hashIndex) ^ (City != null ? City.GetHashCode() : 0);
            result = (result * hashIndex) ^ (Zip != null ? Zip.GetHashCode() : 0);
            return result;
        }
        #endregion
    }
}