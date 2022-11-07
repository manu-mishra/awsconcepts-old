using Application.Calculator.Commands;

namespace AwsConceptsRootLambda.Controllers;

public class CalculatorController : ApiControllerBase
{
    // GET calculator/add/4/2/
    [HttpGet("add/{x}/{y}")]
    public async Task <int> Add(int x, int y)
    {
        return await Mediator.Send(new AddCommand(x, y));
    }

    // GET calculator/substract/4/2/
    [HttpGet("subtract/{x}/{y}")]
    public int Subtract(int x, int y)
    {
        Trace.TraceInformation($"{x} subtract {y} is {x - y}");
        return x - y;
    }

    // GET calculator/multiply/4/2/
    [HttpGet("multiply/{x}/{y}")]
    public int Multiply(int x, int y)
    {
        Trace.TraceInformation($"{x} multiply {y} is {x * y}");
        return x * y;
    }

    // GET calculator/divide/4/2/
    [HttpGet("divide/{x}/{y}")]
    public int Divide(int x, int y)
    {
        Trace.TraceInformation($"{x} divide {y} is {x / y}");
        return x / y;
    }
}
