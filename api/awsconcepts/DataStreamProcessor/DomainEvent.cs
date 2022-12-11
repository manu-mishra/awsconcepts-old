namespace DataStreamProcessor
{
    public class DomainEvent
    {
        public bool ShouldProcess = false;
        public string RecordType { get; set; }
        public string RecordJson { get; set; }
        public DateTime? EventTime { get; set; }
        public string SequenceNumber { get; set; }
        public string EventType { get; set; }
    }
}
