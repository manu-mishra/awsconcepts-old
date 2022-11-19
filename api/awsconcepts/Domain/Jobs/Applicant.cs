namespace Domain.Jobs
{
    public class Applicant
    {
        public Applicant(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
