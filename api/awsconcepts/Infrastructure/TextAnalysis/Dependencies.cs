using Amazon.Comprehend;
using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.TextAnalysis
{
    internal static class Dependencies
    {
        internal static IServiceCollection WithTextAnalysisDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonComprehend>(_ => new AmazonComprehendClient());
            services.AddScoped(typeof(ITextAnalysisProvider), typeof(AwsComprehendAnalyzer));
            return services;
        }
    }
}
