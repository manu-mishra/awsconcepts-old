﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.DynamoDBEvents;
using System.Text.Json;

namespace DataStreamProcessor
{
    public static class DynamoDbExtensions
    {
        public static DomainEvent GetDomainEvent(this DynamoDBEvent.DynamodbStreamRecord record)
        {
            if (record.EventName == OperationType.INSERT || record.EventName == OperationType.MODIFY)
            {
                string? recordType = record.Dynamodb.NewImage["etype"].S;
                if (recordType != null)
                {
                    Document itemAsDocument = Document.FromAttributeMap(record.Dynamodb.NewImage);
                    string recordJson = itemAsDocument.ToJson();
                    return new DomainEvent() { ShouldProcess = true, RecordType = recordType, RecordJson = recordJson };
                }
                else
                    return new DomainEvent() { ShouldProcess = false, RecordType = "Ignored", RecordJson = "Ignored" };
            }
            Console.WriteLine(JsonSerializer.Serialize(record));
            return new DomainEvent() { ShouldProcess = false, RecordType = "Ignored", RecordJson = "Ignored" };
        }
    }
}
