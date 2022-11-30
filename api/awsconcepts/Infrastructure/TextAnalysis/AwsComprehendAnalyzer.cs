using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Application.Common.Interfaces;
using Domain.ValueTypes;

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
            var response = new List<Domain.ValueTypes.TextAnalysis>();
            var detectEntitiesRequest = new DetectEntitiesRequest()
            {
                Text = Text,
                LanguageCode = "en",
            };
            var detectEntitiesResponse = await comprehendClient.DetectEntitiesAsync(detectEntitiesRequest);
            foreach (var e in detectEntitiesResponse.Entities)
            {
                response.Add(new Domain.ValueTypes.TextAnalysis { Type = e.Type, Text = e.Text, Score = e.Score, BeginOffset = e.BeginOffset, EndOffset = e.EndOffset });
            }
            return response;
        }
        public async Task<List<Domain.ValueTypes.TextAnalysis>> AnalysePii(string Text, CancellationToken cancellationToken)
        {
            var response = new List<Domain.ValueTypes.TextAnalysis>();

            var detectPiiEntitiesRequest = new DetectPiiEntitiesRequest()
            {
                Text = Text,
                LanguageCode = "en",
            };
            var detectPiiResponse = await comprehendClient.DetectPiiEntitiesAsync(detectPiiEntitiesRequest);

            foreach (var e in detectPiiResponse.Entities)
            {
                response.Add(new Domain.ValueTypes.TextAnalysis { 
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
