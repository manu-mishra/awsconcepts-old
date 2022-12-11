using Microsoft.Extensions.Configuration;
using System.Text.Json;
using playground.Jobs;
using playground.UserSetup;
using Amazon.Lambda.DynamoDBEvents;
using DataStreamProcessor;

var configuration = new ConfigurationBuilder()
  .AddUserSecrets<Program>()
  .Build();
//Indexer.TryIndex();

//var document = new Domain.Applicants.ProfileDocumentDetail() { Id = "howdy", DocumentText = "Dowdy", Name = "Rowdy" };
//var serializedDoc = JsonSerializer.Serialize(document);
//await DataStreamProcessor.Indexer.Index(new DataStreamProcessor.DomainEvent() { RecordType = "Domain.Applicants.ProfileDocumentDetail", RecordJson = serializedDoc, ShouldProcess = true });



//await UserCreator.createUsers(configuration);

//await JobsDataImport.PostJobsFromArmenia(configuration);



using FileStream stream = File.OpenRead("data/sampleDynamoDbPacket.json");

var evt = JsonSerializer.Deserialize<DynamoDBEvent.DynamodbStreamRecord>(stream);

var domainEvent = evt.GetDomainEvent();

Console.ReadLine();
