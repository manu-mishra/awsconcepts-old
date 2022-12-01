using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Application.Common.Interfaces;

namespace Infrastructure.TextAnalysis
{
    internal class AwsComprehendAnalyzer : ITextAnalysisProvider
    {
        private readonly IAmazonComprehend comprehendClient;

        public AwsComprehendAnalyzer(IAmazonComprehend amazonComprehend)
        {
            this.comprehendClient = amazonComprehend;
        }
        public async Task<List<Domain.ValueTypes.TextAnalysis>> AnalyseText(string Text, CancellationToken cancellationToken)
        {
            List<Domain.ValueTypes.TextAnalysis> response = new List<Domain.ValueTypes.TextAnalysis>();
            DetectEntitiesRequest detectEntitiesRequest = new DetectEntitiesRequest()
            {
                Text = Text,
                LanguageCode = "en",
            };
            DetectEntitiesResponse detectEntitiesResponse = await comprehendClient.DetectEntitiesAsync(detectEntitiesRequest);
            foreach (Entity? e in detectEntitiesResponse.Entities)
            {
                response.Add(new Domain.ValueTypes.TextAnalysis { Type = e.Type, Text = e.Text, Score = e.Score, BeginOffset = e.BeginOffset, EndOffset = e.EndOffset });
            }
            return response;
        }
        public async Task<List<Domain.ValueTypes.TextAnalysis>> AnalysePii(string Text, CancellationToken cancellationToken)
        {
            List<Domain.ValueTypes.TextAnalysis> response = new List<Domain.ValueTypes.TextAnalysis>();

            DetectPiiEntitiesRequest detectPiiEntitiesRequest = new DetectPiiEntitiesRequest()
            {
                Text = Text,
                LanguageCode = "en",
            };
            DetectPiiEntitiesResponse detectPiiResponse = await comprehendClient.DetectPiiEntitiesAsync(detectPiiEntitiesRequest);

            foreach (PiiEntity? e in detectPiiResponse.Entities)
            {
                response.Add(new Domain.ValueTypes.TextAnalysis
                {
                    Type = e.Type,
                    Score = e.Score,
                    BeginOffset = e.BeginOffset,
                    EndOffset = e.EndOffset,
                    Text = Text.Substring(e.BeginOffset, e.EndOffset - e.BeginOffset)
                });
            }
            return response;
        }
    }
}
