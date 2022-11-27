namespace Application.Applicant.Dto
{
    public class ApplicantProfile
    {
        public ApplicantProfile(string IdentityId, string Id, string Name, string ProfileDocumentId, string ProfileHighlights, string ProfileText, string[] Skills)
        {
            this.IdentityId = IdentityId;
            this.Id = Id;
            this.Name = Name;
            this.ProfileDocumentId = ProfileDocumentId;
            this.ProfileHighlights = ProfileHighlights;
            this.ProfileText = ProfileText;
            this.Skills = Skills;
        }
        public string IdentityId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileHighlights { get; set; }
        public string ProfileText { get; set; }
        public string[] Skills { get; set; }
        public string ProfileDocumentId { get; set; }
    }
}
