namespace Domain.ValueTypes
{
    public class TextAnalysis
    {

        public int BeginOffset { get; set; }

        public int EndOffset { get; set; }

        public float Score { get; set; }

        public string Text { get; set; }


        public string Type { get; set; }


    }
}
