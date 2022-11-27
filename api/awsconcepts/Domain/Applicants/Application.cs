namespace Domain.Applicants
{
    public class Application
    {
        public Application(string Id, string IdentityId, string ProfileId, Profile Profile, Job Job)
        {
            this.Id = Id;
            this.IdentityId = IdentityId;
            this.ProfileId = ProfileId;
            this.Profile = Profile;
            this.Job = Job;
        }

        public string Id { get; }
        public string IdentityId { get; }
        public string ProfileId { get; }
        public Profile Profile { get; }
        public Job Job { get; }
    }
}
