namespace Application.Calculator.Commands;

public class AddCommand : IRequest<int>
{
    public AddCommand(int a, int b)
    {
        A = a;
        B = b;
    }

    public int A { get; }
    public int B { get; }

    public class CreateFeatureCommandHandler : IRequestHandler<AddCommand, int>
    {
        public Task<int> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(request.A + request.B);
        }
    }
}


