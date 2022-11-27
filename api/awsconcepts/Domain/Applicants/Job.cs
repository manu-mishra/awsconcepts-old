namespace Domain.Applicants
{
    public class Job
    {
        public Job(string JobId, string JobTitle, string JobDescription, string CompanyId, string CompanyName)
        {
            this.JobId = JobId;
            this.JobTitle = JobTitle;
            this.JobDescription = JobDescription;
            this.CompanyId = CompanyId;
            this.CompanyName = CompanyName;
        }

        public string JobId { get; }
        public string JobTitle { get; }
        public string JobDescription { get; }
        public string CompanyId { get; }
        public string CompanyName { get; }
    }
}
