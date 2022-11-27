namespace Application.Applicant.Dto
{
    public class ApplicantProfileSummary
    {
        public ApplicantProfileSummary(string Id, string Name, string ProfileHighlights)
        {

            this.Id = Id;
            this.Name = Name;
            this.ProfileHighlights = ProfileHighlights;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileHighlights { get; set; }
    }
}
