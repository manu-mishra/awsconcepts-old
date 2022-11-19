namespace Infrastructure.StorageModel
{
    internal class DynamoDbStorageMapLookUp
    {
        public DynamoDbStorageMapLookUp()
        {

        }
        public Dictionary<Type, StorageKeyMap> RepoConfig { get; private set; } = new Dictionary<Type, StorageKeyMap>();
    }
}
