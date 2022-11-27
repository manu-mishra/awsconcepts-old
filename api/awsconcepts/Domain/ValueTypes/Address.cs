namespace Domain.ValueTypes
{
    public class Address
    {
        public Address(string AddressLine1, string AddressLine2, string State, string Pincode, string Country)
        {
            this.AddressLine1 = AddressLine1;
            this.AddressLine2 = AddressLine2;
            this.State = State;
            this.Pincode = Pincode;
            this.Country = Country;
        }

        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string State { get; }
        public string Pincode { get; }
        public string Country { get; }
    }
}
