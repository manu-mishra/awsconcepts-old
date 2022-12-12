using Application.Organizations.Dto;
using CsvHelper;
using System.Globalization;

namespace playground.Jobs
{
    internal static class NaukriJobsReader
    {
        public static List<(Organization, List<Application.Jobs.Dto.Job>)> ReadCsv()
        {
            var response = new List<(Organization, List<Application.Jobs.Dto.Job>)>();
            using (var reader = new StreamReader("Data/NaukriJobs.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<NaukriJobPost>().ToList();
                var byCompany = records.GroupBy(x => x.company);
                foreach (var orgGroup in byCompany)
                {
                    var orgId = Guid.NewGuid().ToString();
                    var orgDetails= "AnyCompany Jobs";
                    var organization = new Organization(orgGroup.Key, orgDetails, orgId);

                    List<Application.Jobs.Dto.Job> jobs = orgGroup.Select(x =>
                    new Application.Jobs.Dto.Job
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = x.jobdescription,
                        Title = x.jobtitle,
                        Categories = new string[] {x.industry, x.education, x.joblocation_address },
                        Skills= new string[] { x.skills, x.experience }
                    }).ToList();
                    response.Add((organization, jobs));
                }
            }
            return response;
        }
        public static List<string> GetTitles()
        {
            using (var reader = new StreamReader("Data/NaukriJobs.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<NaukriJobPost>().Select(x => x.jobtitle).ToList();

            }
        }
    }
    public class NaukriJobPost
    {
        public string company { get; set; }
        public string education { get; set; }
        public string experience { get; set; }
        public string industry { get; set; }
        public string jobdescription { get; set; }
        public string jobid { get; set; }
        public string joblocation_address { get; set; }
        public string jobtitle { get; set; }
        public string numberofpositions { get; set; }
        public string payrate { get; set; }
        public string postdate { get; set; }
        public string site_name { get; set; }
        public string skills { get; set; }
        public string uniq_id { get; set; }
    }
}
