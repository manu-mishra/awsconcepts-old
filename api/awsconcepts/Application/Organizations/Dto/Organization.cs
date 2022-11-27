namespace Application.Organizations.Dto
{
    public class Organization
    {
        public Organization(string Name, string Details, string? Id)
        {
            this.Name = Name;
            this.Details = Details;
            this.Id = Id;
        }

        public string? Id { get; }
        public string Name { get; }
        public string Details { get; }
    }
}
