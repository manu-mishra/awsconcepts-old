using Amazon.DynamoDBv2;
using Application.Jobs.Dto;
using Application.Organizations.Dto;
using CsvHelper;
using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playground.Jobs
{
    internal static class ArmenianJobsReader
    {
        public static List<(Organization, List<Application.Jobs.Dto.Job>)> ReadCsv()
        {
            var response = new List<(Organization, List<Application.Jobs.Dto.Job>)>();
            using (var reader = new StreamReader("Data/ArmenianJobPosts.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ArmenianJobPost>().ToList();
                var byCompany = records.GroupBy(x => x.Company);
                foreach (var orgGroup in byCompany)
                {
                    var orgId = Guid.NewGuid().ToString();
                    var orgDetails= orgGroup.FirstOrDefault(x => !string.IsNullOrEmpty(x.AboutC))?.AboutC ??"";
                    var organization = new Organization(orgGroup.Key, orgDetails, orgId);

                    List<Application.Jobs.Dto.Job> jobs = orgGroup.Select(x =>
                    new Application.Jobs.Dto.Job
                    {
                        Id = Guid.NewGuid().ToString(),
                        Description = x.JobDescription,
                        Title = x.Title,
                    }).ToList();
                    response.Add((organization, jobs));
                }
            }
            return response;
        }
    }
    public class ArmenianJobPost
    {
        public string jobpost { get; set; }
        public string date { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string AnnouncementCode { get; set; }
        public string Term { get; set; }
        public string Eligibility { get; set; }
        public string Audience { get; set; }
        public string StartDate { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirment { get; set; }
        public string RequiredQual { get; set; }
        public string Salary { get; set; }
        public string ApplicationP { get; set; }
        public string OpeningDate { get; set; }
        public string Deadline { get; set; }
        public string Notes { get; set; }
        public string AboutC { get; set; }
        public string Attach { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string IT { get; set; }
    }
}
