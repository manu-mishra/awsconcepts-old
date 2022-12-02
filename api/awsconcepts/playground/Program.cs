using playground;
using System.Text.Json;

Environment.SetEnvironmentVariable("elasticUserName", "manu");
Environment.SetEnvironmentVariable("elasticPassword", "Jasmine-01");

Indexer.TryIndex();

//var document = new Domain.Applicants.ProfileDocumentDetail() { Id = "howdy", DocumentText = "Dowdy", Name = "Rowdy" };
//var serializedDoc = JsonSerializer.Serialize(document);
//await DataStreamProcessor.Indexer.Index(new DataStreamProcessor.DomainEvent() { RecordType = "Domain.Applicants.ProfileDocumentDetail", RecordJson = serializedDoc, ShouldProcess = true });

Console.ReadLine();
