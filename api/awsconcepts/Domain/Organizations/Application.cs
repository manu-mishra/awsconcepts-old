namespace Domain.Organizations
{
    public class Application
    {
        public Application(string Id, string ProfileId, string Name, string ProfileHighlights, string Title, string Location)
        {
            this.Id = Id;
            this.ProfileId = ProfileId;
            this.Name = Name;
            this.ProfileHighlights = ProfileHighlights;
            this.Title = Title;
            this.Location = Location;
        }
        public string Id { get; set; }
        public string ProfileId { get; }
        public string Name { get; set; }
        public string ProfileHighlights { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
    }
}
