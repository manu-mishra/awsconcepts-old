namespace Application.Values.Queries
{
    public class GetValuesQuery : IRequest<IEnumerable<string>>
    {
    }
    public class GetValuesQueryHandler : IRequestHandler<GetValuesQuery, IEnumerable<string>>
    {

        public Task<IEnumerable<string>> Handle(GetValuesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<string>>(new List<string> { "hello", "world" });
        }
    }
}
