namespace Domain.ValueTypes
{
    public class PhoneNumber
    {
        public PhoneNumber(string ContactNumber, string CountryCode)
        {
            this.ContactNumber = ContactNumber;
            this.CountryCode = CountryCode;
        }

        public string ContactNumber { get; }
        public string CountryCode { get; }
    }
}
