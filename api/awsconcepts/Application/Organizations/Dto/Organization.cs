namespace Application.Organizations.Dto
{
    public class Organization
    {
        public Organization(string Name, string Details)
        {
            this.Name = Name;
            this.Details = Details;
        }

        public string? Id { get; }
        public string Name { get; }
        public string Details { get; }
    }
}
