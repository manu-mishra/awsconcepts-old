namespace Application.Applicant.Dto
{
    public class ApplicantProfileDraft
    {
        public ApplicantProfileDraft(string Id, string Name, string ProfileDocumentId, string ProfileHighlights)
        {
            this.Id = Id;
            this.Name = Name;
            this.ProfileDocumentId = ProfileDocumentId;
            this.ProfileHighlights = ProfileHighlights;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string? ProfileHighlights { get; set; }
        public string? ProfileText { get; set; }
        public string[]? Skills { get; set; }
        public string? ProfileDocumentId { get; set; }
    }
}
