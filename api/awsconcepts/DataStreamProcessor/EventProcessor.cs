namespace DataStreamProcessor
{
    internal static class EventProcessor
    {
        public static async Task ProcessEvent(this DomainEvent domainEvent)
        {
            await Indexer.Index(domainEvent);
        }
    }
}
