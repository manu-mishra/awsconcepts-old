namespace DataStreamProcessor
{
    public class DomainEvent
    {
        public bool ShouldProcess = false;
        public string RecordType { get; set; }
        public string RecordJson { get; set; }
    }
}
