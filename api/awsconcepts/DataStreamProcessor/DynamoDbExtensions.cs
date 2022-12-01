using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.DynamoDBEvents;

namespace DataStreamProcessor
{
    public static class DynamoDbExtensions
    {
        public static DomainEvent GetDomainEvent(this DynamoDBEvent.DynamodbStreamRecord record)
        {
            var recordType = record.Dynamodb.NewImage["etype"].S;
            if (recordType != null && record.EventName == OperationType.INSERT || record.EventName == OperationType.MODIFY)
            {
                Document itemAsDocument = Document.FromAttributeMap(record.Dynamodb.NewImage);
                var recordJson = itemAsDocument.ToJson();
                return new DomainEvent() { ShouldProcess = true, RecordType = recordType, RecordJson = recordJson };
            }
            else
                return new DomainEvent() { ShouldProcess = false, RecordType = "Ignored", RecordJson = "Ignored" };
        }
    }
}
