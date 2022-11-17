namespace Domain.Jobs
{
    public class Applicant
    {
        public Applicant(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
