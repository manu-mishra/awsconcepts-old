using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DataStreamProcessor;

public class Function
{
    public void FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
    {
        foreach (DynamoDBEvent.DynamodbStreamRecord? record in dynamoEvent.Records)
        {
            context.Logger.LogInformation($"record: {JsonSerializer.Serialize(record)}");
        }
    }
}