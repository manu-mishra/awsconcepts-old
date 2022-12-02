using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using System.Text;
using System.Text.Json;

namespace DataStreamProcessor
{
    internal static class StreamWriter
    {
        static AmazonKinesisClient streamClient;

        static StreamWriter()
        {
            streamClient = new AmazonKinesisClient();
        }
        public static async Task Stream(DomainEvent domainEvent)
        {
            if (domainEvent != null && domainEvent.ShouldProcess)
            {
                string partitionKey = JsonDocument.Parse(domainEvent.RecordJson).RootElement.GetProperty("Id").ToString();
                using MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent.RecordJson)));
                PutRecordRequest request = new PutRecordRequest()
                {
                    StreamName = "awsconcepts",
                    PartitionKey = partitionKey,
                    Data = ms
                };
                PutRecordResponse response = await streamClient.PutRecordAsync(request);
                Console.WriteLine($"response code {response.HttpStatusCode}");

            }
        }
    }
}
