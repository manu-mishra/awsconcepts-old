using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.Common.Interfaces;
using Infrastructure.Config;
using System.Text.Json;

namespace Infrastructure.Repository
{
    internal class EntityRepository<DomainEntity> : IEntityRepository<DomainEntity>
    {
        private readonly IAmazonDynamoDB dynamoDB;
        private readonly string pkPropertyName;
        private readonly string pkPrefix;
        private readonly string skPropertyName;
        private readonly string skPrefix;
        private readonly bool useSecondaryIndex;

        public EntityRepository(IAmazonDynamoDB dynamoDB, EntityConfigLookUp repositoryConfigLookUp)
        {
            this.dynamoDB = dynamoDB;
            EntityConfig config = repositoryConfigLookUp.RepoConfig[typeof(DomainEntity)]; ;
            pkPropertyName = config.PkPropertyName;
            pkPrefix = config.PkPrefix;
            skPropertyName = config.SkPropertyName;
            skPrefix = config.SkPrefix;
            useSecondaryIndex = config.UseSecondaryIndex;
        }
        public async Task Delete(string EntityId, string ScopeId, CancellationToken CancellationToken)
        {
            if (useSecondaryIndex)
                throw new InvalidOperationException($"could not perform {nameof(Delete)} on {typeof(DomainEntity)} using secondary index");
            DeleteItemRequest request = new DeleteItemRequest
            {
                TableName = "awsconcepts",
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ek", new AttributeValue { S = pkPrefix + EntityId} },
                    { "sk", new AttributeValue { S = skPrefix + ScopeId} }
                },
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
            };
            DeleteItemResponse response = await dynamoDB.DeleteItemAsync(request, CancellationToken);
        }

        public async Task<DomainEntity?> Get(string EntityId, string ScopeId, CancellationToken CancellationToken)
        {
            GetItemRequest request = new GetItemRequest
            {
                TableName = "awsconcepts",
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ek", new AttributeValue { S = pkPrefix + EntityId} },
                    { "sk", new AttributeValue { S = skPrefix + ScopeId} }
                },
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
            };

            GetItemResponse response = await dynamoDB.GetItemAsync(request, CancellationToken);
            TraceConsumedCapacity(response.ConsumedCapacity);
            if (response.Item.Count == 0)
            {
                return default;
            }

            Document itemAsDocument = Document.FromAttributeMap(response.Item);
            return JsonSerializer.Deserialize<DomainEntity>(itemAsDocument.ToJson());
        }

        public async Task<(List<DomainEntity>, string?)> GetAll(string ScopeId, string? ContinuationToken, CancellationToken CancellationToken)
        {
            List<DomainEntity> result = new List<DomainEntity>();
            string? continuationToken = default(string);
            QueryRequest query = new QueryRequest()
            {
                TableName = "awsconcepts",
                IndexName = "sk-pk-index",
                KeyConditionExpression = "sk = :v_sk",
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL,
                Limit = 5,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    //{":v_sk", new AttributeValue{S = ScopeId} }
                     {":v_sk", new AttributeValue{S = skPrefix + ScopeId} }
                },

            };
            if (!string.IsNullOrEmpty(ContinuationToken))
            {
                //query.ExclusiveStartKey = Convert.FromBase64CharArray(ContinuationToken)
            }

            QueryResponse response = await dynamoDB.QueryAsync(query, CancellationToken);
            TraceConsumedCapacity(response.ConsumedCapacity);
            foreach (Dictionary<string, AttributeValue>? item in response.Items)
            {
                Document itemAsDocument = Document.FromAttributeMap(item);
                DomainEntity? entity = JsonSerializer.Deserialize<DomainEntity>(itemAsDocument.ToJson());
                if (entity is not null)
                    result.Add(entity);
            }
            //dbresponse.las
            //continuationToken = string.IsNullOrWhiteSpace(dbresponse.LastEvaluatedKey) ??
            //    Convert.ToBase64String(Encoding.UTF8.GetBytes(dbresponse.LastEvaluatedKey));

            return (result, continuationToken);
        }

        public async Task<bool> Put(DomainEntity DomainEntity, CancellationToken CancellationToken)
        {
            if (useSecondaryIndex)
                throw new InvalidOperationException($"could not perform {nameof(Put)} on {typeof(DomainEntity)} using secondary index");
            string entityJson = JsonSerializer.Serialize(DomainEntity);
            Document entityDocument = Document.FromJson(entityJson);
            Dictionary<string, AttributeValue> entityAsAttibute = entityDocument.ToAttributeMap();

            entityAsAttibute["ek"] = new AttributeValue { S = pkPrefix + entityAsAttibute[pkPropertyName].S };
            entityAsAttibute["sk"] = new AttributeValue { S = skPrefix + entityAsAttibute[skPropertyName].S };
            entityAsAttibute["etype"] = new AttributeValue { S = typeof(DomainEntity).ToString() };
            entityAsAttibute["etag"] = new AttributeValue { S = typeof(DomainEntity).ToString() };
            entityAsAttibute["indexable"] = new AttributeValue { S = typeof(DomainEntity).ToString() };

            PutItemRequest putItemRequest = new PutItemRequest
            {
                TableName = "awsconcepts",
                Item = entityAsAttibute,
                ReturnConsumedCapacity = ReturnConsumedCapacity.TOTAL
            };

            PutItemResponse response = await dynamoDB.PutItemAsync(putItemRequest, CancellationToken);
            TraceConsumedCapacity(response.ConsumedCapacity);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        private void TraceConsumedCapacity(ConsumedCapacity ConsumedCapacity)
        {
            System.Diagnostics.Trace.WriteLine(ConsumedCapacity);
        }
    }
}
