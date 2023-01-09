using Application.Organizations.Dto;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace playground.Jobs
{
    internal static class JobsDataImport
    {
        private static int totalJobsCreated;
        private static int totalOrgsCreated;
        public static async Task PostJobsFromArmenia(IConfigurationRoot configuration)
        {
            var password = configuration["DefaultUserPassword"];
            var tasks = new List<Task>();
            var results = ArmenianJobsReader.ReadCsv();
            var splitList = results.Split(40);
            for (int i = 1; i <= splitList.Count(); i++)
            {
                var processList = splitList[i - 1];
                //await ImportJobs(i, processList, password);
                tasks.Add(ImportJobs(i, processList, password));

            }
            await Task.WhenAll(tasks);
        }

        public static async Task PostJobsFromNaukri(IConfigurationRoot configuration)
        {
            var password = configuration["DefaultUserPassword"];
            var tasks = new List<Task>();
            var results = NaukriJobsReader.ReadCsv();
            var splitList = results.Split(40);
            for (int i = 1; i <= splitList.Count(); i++)
            {
                var processList = splitList[i - 1];
                //await ImportJobs(i, processList, password);
                tasks.Add(ImportJobs(i, processList, password));

            }
            await Task.WhenAll(tasks);
        }
        public static async Task PostJobsFromAmazon(IConfigurationRoot configuration)
        {
            var password = configuration["DefaultUserPassword"];
            var results = AmazonJobsReader.ReadCsv();
            await ImportJobsAmazon(1, results, password);
        }
        static async Task ImportJobs(int userNumber, IEnumerable<(Organization, List<Application.Jobs.Dto.Job>)> jobs, string password)
        {
            try
            {
                Console.WriteLine("processing for " + userNumber + $" : {jobs.Count()} orgs will be created");
                var token = await GetUserIdToken(userNumber, password);
                foreach (var item in jobs)
                {
                    Console.WriteLine("processing Org for " + userNumber + $" : {item.Item2.Count()} jobs will be created");
                    var response =  await "https://www.awsconcepts.com/api/Organizations".WithOAuthBearerToken(token).PutJsonAsync(item.Item1);
                    totalOrgsCreated++;
                    foreach (var job in item.Item2)
                    {
                        response = await("https://www.awsconcepts.com/api/Organizations/" + item.Item1.Id + "/jobs").WithOAuthBearerToken(token).PutJsonAsync(job);
                        totalJobsCreated++;
                    }
                    Console.Write($"Total Orgs : {totalOrgsCreated} and Jobs {totalJobsCreated}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        static async Task ImportJobsAmazon(int userNumber, IEnumerable<(Organization, List<Application.Jobs.Dto.Job>)> jobs, string password)
        {
            try
            {
                Console.WriteLine("processing for " + userNumber + $" : {jobs.Count()} orgs will be created");
                var token = await GetMainUserToken(password);
                foreach (var item in jobs)
                {
                    Console.WriteLine("processing Org for " + userNumber + $" : {item.Item2.Count()} jobs will be created");
                    var response = await "https://www.awsconcepts.com/api/Organizations".WithOAuthBearerToken(token).PutJsonAsync(item.Item1);
                    totalOrgsCreated++;
                    foreach (var job in item.Item2)
                    {
                        response = await ("https://www.awsconcepts.com/api/Organizations/" + item.Item1.Id + "/jobs").WithOAuthBearerToken(token).PutJsonAsync(job);
                        totalJobsCreated++;
                    }
                    Console.Write($"Total Orgs : {totalOrgsCreated} and Jobs {totalJobsCreated}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static void GetUniqueJobTitles()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            var jobTitles= ArmenianJobsReader.GetTitles();
            jobTitles.AddRange(AmazonJobsReader.GetTitles());
            jobTitles.AddRange(NaukriJobsReader.GetTitles());
            var distinctTitles = jobTitles.Distinct().Select(x=>new {Title=x });
            var result =  JsonSerializer.Serialize(distinctTitles);
        }

        static async Task<string> GetUserIdToken(int userNumber, string password)
        {
            var response = await "https://cognito-idp.us-east-1.amazonaws.com/"
                .WithHeader("X-Amz-Target", "AWSCognitoIdentityProviderService.InitiateAuth")
                .WithHeader("Content-Type", "application/x-amz-json-1.1")
                .PostJsonAsync(new { AuthParameters = new { USERNAME = $"heymanu+{userNumber}@amazon.com", PASSWORD = password }, AuthFlow = "USER_PASSWORD_AUTH", ClientId = "2h9kj09dl1cb9t0aqjtho3jddc" })
                .ReceiveJson<CognitoResponse>();
            return response.AuthenticationResult.IdToken;
        }

        static async Task<string> GetMainUserToken( string password)
        {
            var response = await "https://cognito-idp.us-east-1.amazonaws.com/"
                .WithHeader("X-Amz-Target", "AWSCognitoIdentityProviderService.InitiateAuth")
                .WithHeader("Content-Type", "application/x-amz-json-1.1")
                .PostJsonAsync(new { AuthParameters = new { USERNAME = $"heymanu@amazon.com", PASSWORD = password }, AuthFlow = "USER_PASSWORD_AUTH", ClientId = "2h9kj09dl1cb9t0aqjtho3jddc" })
                .ReceiveJson<CognitoResponse>();
            return response.AuthenticationResult.IdToken;
        }

        static List<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
        {
            int i = 0;
            var splits = from item in list
                         group item by i++ % parts into part
                         select part.AsEnumerable();
            return splits.ToList();
        }
        public class CognitoResponse { public AuthenticationResult AuthenticationResult { get; set; } }
        public class AuthenticationResult
        {
            public string IdToken { get; set; }
        }
    }
}
