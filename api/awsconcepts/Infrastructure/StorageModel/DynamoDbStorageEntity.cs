using System.Text.Json.Serialization;

namespace Infrastructure.StorageModel
{
    public sealed class DynamoDbStorageEntity<T>
    {
        public DynamoDbStorageEntity(T entity, string primaryKey, string sortKey)
        {
            PrimaryKey = primaryKey;
            SortKey = sortKey;
            Entity = entity;
            EntityType = typeof(T).ToString();
            Timestamp = DateTime.UtcNow.Ticks;
        }

        [JsonPropertyName("pk")]
        public string PrimaryKey { get; set; }

        [JsonPropertyName("sk")]
        public string SortKey { get; set; }
        [JsonPropertyName("entity")]
        public T Entity { get; set; }
        [JsonPropertyName("entityType")]
        public string EntityType { get; set; }
        
        [JsonPropertyName("timeStamp")]
        public long Timestamp { get; set; }


    }
}
