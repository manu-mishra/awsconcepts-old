using Microsoft.Extensions.Configuration;
using playground.Jobs;
using playground.UserSetup;

var configuration = new ConfigurationBuilder()
  .AddUserSecrets<Program>()
  .Build();
//Indexer.TryIndex();

//var document = new Domain.Applicants.ProfileDocumentDetail() { Id = "howdy", DocumentText = "Dowdy", Name = "Rowdy" };
//var serializedDoc = JsonSerializer.Serialize(document);
//await DataStreamProcessor.Indexer.Index(new DataStreamProcessor.DomainEvent() { RecordType = "Domain.Applicants.ProfileDocumentDetail", RecordJson = serializedDoc, ShouldProcess = true });



//await UserCreator.createUsers(configuration);

await JobsDataImport.PostJobsFromArmenia(configuration);

Console.ReadLine();
