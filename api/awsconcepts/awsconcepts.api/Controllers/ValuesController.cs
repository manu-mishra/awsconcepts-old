using Application.Values.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace awsconcepts.api.Controllers;

public class ValuesController : ApiController
{
    // GET api/values
    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        return await Mediator.Send(new GetValuesQuery());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}