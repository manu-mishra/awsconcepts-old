namespace Domain.Applicants
{
    public class ProfileDraft
    {
        public ProfileDraft(string IdentityId, string Id, string ProfileName)
        {
            this.IdentityId = IdentityId;
            this.Id = Id;
            this.ProfileName = ProfileName;

        }
        public string IdentityId { get; set; }
        public string Id { get; set; }
        public string ProfileName { get; set; }
        public string? Name { get; set; }
        public string? ProfileHighlights { get; set; }
        public string? ProfileText { get; set; }
        public string[]? Skills { get; set; }
        public string? ProfileDocumentId { get; set; }
    }
}
