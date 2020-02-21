namespace Module4.DomainModel.DomainEvents
{
    public class Party : Aggregate
    {
        public string VatIndex { get; protected set; }
        public string NationalIdentificationNumber { get; protected set; }
    }
}
