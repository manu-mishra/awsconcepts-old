namespace Domain.Organizations
{
    public class Job
    {
        public Job(string id, string organizationId, string title, string description, string[]? categories = default, string[]? skills = default)
        {
            Title = title;
            Id = id;
            OrganizationId = organizationId;
            Description = description;
            Categories = categories;
            Skills = skills;
        }
        public string Id { get; set; }
        public string OrganizationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[]? Categories { get; set; }
        public string[]? Skills { get; set; }
    }
}
