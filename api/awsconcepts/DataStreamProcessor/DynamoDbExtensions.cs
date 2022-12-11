using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.DynamoDBEvents;
using System.Text.Json;

namespace DataStreamProcessor
{
    public static class DynamoDbExtensions
    {
        public static DomainEvent GetDomainEvent(this DynamoDBEvent.DynamodbStreamRecord record)
        {
            if (record.EventName == OperationType.INSERT || record.EventName == OperationType.MODIFY || record.EventName == OperationType.REMOVE)
            {
                string? recordType = (record.EventName == OperationType.INSERT || record.EventName == OperationType.MODIFY)
                    ?record.Dynamodb.NewImage["etype"].S: record.Dynamodb.OldImage["etype"].S;
                if (recordType != null)
                {
                    Document itemAsDocument = (record.EventName == OperationType.REMOVE) ?
                        Document.FromAttributeMap(record.Dynamodb.OldImage)
                        : Document.FromAttributeMap(record.Dynamodb.NewImage);
                    itemAsDocument.Add("EventTime", new Primitive(){Value = record.Dynamodb.ApproximateCreationDateTime});
                    itemAsDocument.Add("EventType", new Primitive { Value = record.EventName.Value });
                    itemAsDocument.Add("SequenceNumber", new Primitive { Value = record.Dynamodb.SequenceNumber });

                    string recordJson = itemAsDocument.ToJson();
                    return new DomainEvent()
                    {
                        ShouldProcess = true,
                        RecordType = recordType,
                        RecordJson = recordJson,
                        EventTime = record.Dynamodb.ApproximateCreationDateTime.ToUniversalTime(),
                        EventType = record.EventName,
                        SequenceNumber = record.Dynamodb.SequenceNumber
                    };
                }
            }
            return new DomainEvent() { ShouldProcess = false, RecordType = "Ignored", RecordJson = "Ignored" };
        }
    }
}
