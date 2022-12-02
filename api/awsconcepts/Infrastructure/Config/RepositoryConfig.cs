namespace Infrastructure.Config
{
    internal class EntityConfig
    {
        public EntityConfig(string pkPropertyName, string skPropertyName, bool useSecondaryIndex = false, string? pkPrefix = default, string? skPrefix = default)
        {
            PkPropertyName = pkPropertyName;
            SkPropertyName = skPropertyName;
            UseSecondaryIndex = useSecondaryIndex;
            PkPrefix = pkPrefix ?? string.Empty;
            SkPrefix = skPrefix ?? string.Empty;
        }
        public string PkPropertyName { get; set; }
        public string PkPrefix { get; set; }
        public string SkPropertyName { get; set; }
        public string SkPrefix { get; set; }
        public bool UseSecondaryIndex { get; set; }
    }
}
