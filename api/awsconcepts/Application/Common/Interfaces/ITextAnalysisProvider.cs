using Domain.ValueTypes;

namespace Application.Common.Interfaces
{
    public interface ITextAnalysisProvider
    {
        Task<List<TextAnalysis>> AnalyseText(string Text, CancellationToken cancellationToken);
        Task<List<TextAnalysis>> AnalysePii(string Text, CancellationToken cancellationToken);
    }
}
