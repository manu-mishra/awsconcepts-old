using Elasticsearch.Net;
using Nest;

var uris = new Uri[]
            {
                new Uri("https://search-awsconcepts-f6zgsd3tkq5fi6hododigdm7vm.us-east-1.es.amazonaws.com/")
            };


var connectionPool = new SniffingConnectionPool(uris);
var settings = new ConnectionSettings(connectionPool)
    .BasicAuthentication("", "")
    .DefaultMappingFor<Testing>(i => i.IndexName("testing"));

var client = new ElasticClient(settings);

var document = new Testing("this is a test", DateTime.Now);

var indexResponse = client.Index(new IndexRequest<Testing>(document, IndexName.From<Testing>(), 1));
if (!indexResponse.IsValid)
{
    Console.Write("Failed to index document. ");
    if (indexResponse.ServerError != null)
    {
        Console.WriteLine(indexResponse.ServerError);
    }
    else if (indexResponse.OriginalException != null)
    {
        Console.WriteLine(indexResponse.OriginalException);
    }
    else
    {
        Console.WriteLine("Error code: " + indexResponse.ApiCall.HttpStatusCode);
    }
}
else
{
    Console.WriteLine($"Indexed document to index \"testing\" with id 1");
}


var getResponse = client.Get<Testing>(new GetRequest<Testing>(1));
if (getResponse.OriginalException != null)
{
    throw getResponse.OriginalException;
}

if (!getResponse.IsValid)
{
    Console.Write("Failed to retrieve document. ");
    if (getResponse.ServerError != null)
    {
        Console.WriteLine(getResponse.ServerError);
    }
    else if (getResponse.OriginalException != null)
    {
        Console.WriteLine(getResponse.OriginalException);
    }
    else
    {
        Console.WriteLine("Error code: " + getResponse.ApiCall.HttpStatusCode);
    }
}
else
{
    Console.WriteLine($"Retrieved document: " +
        $"{{Id: {getResponse.Id}, " +
        $"Description: {getResponse.Source.Description}, " +
        $"Timestamp: {getResponse.Source.Timestamp}}}");
}

Console.ReadLine();

public class Testing
{
    public string Description;
    public DateTime Timestamp;

    public Testing(string Description, DateTime Timestamp)
    {
        this.Description = Description;
        this.Timestamp = Timestamp;
    }
}