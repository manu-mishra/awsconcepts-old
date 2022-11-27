namespace Infrastructure.Repository.Config
{
    internal class RepositoryConfig
    {
        public RepositoryConfig(string pkPropertyName, string skPropertyName, bool useSecondaryIndex = false, string? pkPrefix = default(string), string? skPrefix = default(string))
        {
            PkPropertyName = pkPropertyName;
            SkPropertyName = skPropertyName;
            UseSecondaryIndex = useSecondaryIndex;
            PkPrefix = pkPrefix ?? String.Empty;
            SkPrefix = skPrefix ?? String.Empty;
        }
        public string PkPropertyName { get; set; }
        public string PkPrefix { get; set; }
        public string SkPropertyName { get; set; }
        public string SkPrefix { get; set; }
        public bool UseSecondaryIndex { get; set; }
    }
}
