namespace Application.Applicant.Dto
{
    public class ApplicantProfileDraft
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string? ProfileHighlights { get; set; }
        public string? ProfileText { get; set; }
        public string[]? Skills { get; set; }
        public string? ProfileDocumentId { get; set; }
    }
}
