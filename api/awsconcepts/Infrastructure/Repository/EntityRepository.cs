using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.Common.Interfaces;
using System.Text.Json;

namespace Infrastructure.Repository
{
    internal class EntityRepository<Entity> : IEntityRepository<Entity>
    {
        private readonly IAmazonDynamoDB dynamoDB;

        public EntityRepository(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }
        public Task<bool> Delete(Entity DomainEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<Entity?> Get(string ScopeId, string Id)
        {
            var request = new GetItemRequest
            {
                TableName = "awsconcepts",
                Key = new Dictionary<string, AttributeValue>
            {
                { "ek", new AttributeValue { S = ScopeId} },
                { "sk", new AttributeValue { S = Id } }
            }
            };

            var response = await dynamoDB.GetItemAsync(request);
            if (response.Item.Count == 0)
            {
                return default;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Item);
            return JsonSerializer.Deserialize<Entity>(itemAsDocument.ToJson());
        }

        public async Task<List<Entity>> GetAll(string ScopeId)
        {
            var response = new List<Entity>();
            var request = new BatchGetItemRequest
            {
                RequestItems = new Dictionary<string, KeysAndAttributes>()
                  {
                    { "awsconcepts",
                      new KeysAndAttributes
                      {
                        Keys = new List<Dictionary<string, AttributeValue>>()
                        {
                          new Dictionary<string, AttributeValue>()
                          {
                            { "ek", new AttributeValue { S = "ScopeId" } }
                          }
                        }
                      }
                    }
                  }
            };

            var dbresponse = await dynamoDB.BatchGetItemAsync(request);
            if (dbresponse.Responses["awsconcepts"].Count != 0)
            {
                foreach (var item in dbresponse.Responses["awsconcepts"])
                {
                    var itemAsDocument = Document.FromAttributeMap(item);
                    var entity = JsonSerializer.Deserialize<Entity>(itemAsDocument.ToJson());
                    if (entity is not null)
                        response.Add(entity);
                }
            }

            return response;
        }

        public async Task<bool> Put(Entity DomainEntity)
        {
            var entityJson = JsonSerializer.Serialize(DomainEntity);
            var entityDocument = Document.FromJson(entityJson);
            var entityAsAttibute = entityDocument.ToAttributeMap();
            var putItemRequest = new PutItemRequest
            {
                TableName = "awsconcepts",
                Item = entityAsAttibute
            };

            var response = await dynamoDB.PutItemAsync(putItemRequest);
            System.Diagnostics.Trace.WriteLine(response);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
