namespace Infrastructure.StorageModel
{
    internal class StorageKeyMap
    {
        public StorageKeyMap(string primaryKeyPropertyName, string secondaryKeyPropertyName)
        {
            PrimaryKeyPropertyName = primaryKeyPropertyName;
            SecondaryKeyPropertyName = secondaryKeyPropertyName;
        }
        public string PrimaryKeyPropertyName { get; set; }
        public string SecondaryKeyPropertyName { get; set; }
    }
}
