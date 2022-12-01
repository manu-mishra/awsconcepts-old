using Elasticsearch.Net;
using Nest;
using System.Text.Json;

Environment.SetEnvironmentVariable("elasticUserName", "");
Environment.SetEnvironmentVariable("elasticPassword", "");

var document = new Domain.Applicants.ProfileDocumentDetail(){Id="howdy", DocumentText="Dowdy", Name="Rowdy"};
var serializedDoc=JsonSerializer.Serialize(document);
await DataStreamProcessor.Indexer.Index(new DataStreamProcessor.DomainEvent() { RecordType = "Domain.Applicants.ProfileDocumentDetail", RecordJson= serializedDoc, ShouldProcess=true });

Console.ReadLine();
