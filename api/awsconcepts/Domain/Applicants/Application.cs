namespace Domain.Applicants
{
    public class Application
    {
        public string Id { get; }
        public string IdentityId { get; }
        public string ProfileId { get; }
        public Profile Profile { get; }
        public Job Job { get; }
    }
}
