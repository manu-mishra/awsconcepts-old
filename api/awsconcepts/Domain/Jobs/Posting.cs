namespace Domain.Jobs
{
    public class Posting : IDomainEntity
    {
        public Posting(string id, string title, string description, Owner owner, string[]? categories = default, string[]? skills = default)
        {
            Title = title;
            Id = id;
            Description = description;
            Categories = categories;
            Skills = skills;
            Owner = owner;
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Owner Owner { get; set; }
        public string[]? Categories { get; set; }
        public string[]? Skills { get; set; }
        public string ek => Id;
        public string sk => Owner.Id;
    }
}
