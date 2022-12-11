using Application.Organizations.Dto;
using CsvHelper;
using System.Globalization;

namespace playground.Jobs
{
    internal static class AmazonJobsReader
    {
        public static List<(Organization, List<Application.Jobs.Dto.Job>)> ReadCsv()
        {
            var response = new List<(Organization, List<Application.Jobs.Dto.Job>)>();
            using (var reader = new StreamReader("Data/AmazonJobsDataset.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<AmazonJobPost>().ToList();
                var byCompany = records.GroupBy(x => x.Company);
                foreach (var orgGroup in byCompany)
                {
                    var orgId = "29f6f382-6880-440d-9c83-065ed8cdce11";
                    var orgDetails= "AnyCompany Jobs";
                    var organization = new Organization(orgGroup.Key, orgDetails, orgId);

                    List<Application.Jobs.Dto.Job> jobs = orgGroup.Select(x =>
                    new Application.Jobs.Dto.Job
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = x.DESCRIPTION + Environment.NewLine+x.BASICQUALIFICATIONS+ Environment.NewLine + x.PREFERREDQUALIFICATIONS,
                        Title = x.Title,
                        Categories = new string[] {x.Title },
                        Skills= new string[] {  }
                    }).ToList();
                    response.Add((organization, jobs));
                }
            }
            return response;
        }
    }
    public class AmazonJobPost
    {
        //Id Title   location Posting_date    DESCRIPTION BASICQUALIFICATIONS PREFERREDQUALIFICATIONS
        public string Id { get; set; }
        public string Title { get; set; }
        public string Company = "Amazon";
        public string location { get; set; }
        public string Posting_date { get; set; }
        public string DESCRIPTION { get; set; }
        public string BASICQUALIFICATIONS { get; set; }
        public string PREFERREDQUALIFICATIONS { get; set; }
    }
}
