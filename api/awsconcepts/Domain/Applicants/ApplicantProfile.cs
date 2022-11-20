namespace Domain.Applicants
{
    public class ApplicantProfile : IDomainEntity
    {
        public ApplicantProfile(string UserId, string Id, string Name, string ProfileAddress, string ProfileHighlights, string ProfileText, string[] Skills)
        {
            this.UserId = UserId;
            this.Id = Id;
            this.Name = Name;
            this.ProfileAddress = ProfileAddress;
            this.ProfileHighlights = ProfileHighlights;
            this.ProfileText = ProfileText;
            this.Skills = Skills;
        }
        public string UserId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileHighlights { get; set; }
        public string ProfileText { get; set; }
        public string[] Skills { get; set; }
        public string ProfileAddress { get; set; }

        public string ek => Id;

        public string sk => UserId;
    }
}
