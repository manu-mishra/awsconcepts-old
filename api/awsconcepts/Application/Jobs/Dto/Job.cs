namespace Application.Jobs.Dto
{
    public class Job
    {
        public string? Id { get; set; }
        public string? OrganizationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[]? Categories { get; set; }
        public string[]? Skills { get; set; }
    }
}
