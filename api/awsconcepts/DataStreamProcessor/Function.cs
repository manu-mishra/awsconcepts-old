using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.XRay.Recorder.Handlers.AwsSdk;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DataStreamProcessor;

public class Function
{
    static Function()
    {
        initialize();
    }
        
    static void initialize()
    {
        AWSSDKHandler.RegisterXRayForAllServices();
    }
    public async Task FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
    {
        foreach (DynamoDBEvent.DynamodbStreamRecord? record in dynamoEvent.Records)
        {
            context.Logger.Log($"processing {record.EventName} for record {record.EventID}");
            await record.GetDomainEvent().ProcessEvent();
        }
    }
}