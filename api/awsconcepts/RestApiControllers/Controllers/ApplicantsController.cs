using Application.Applicant.Commands;
using Application.Applicant.Dto;
using Application.Applicant.Queries;

namespace RestApiControllers.Controllers;

public class ApplicantsController : ApiControllerBase
{
    [HttpGet("ProfileDrafts")]
    public async Task<List<ApplicantProfileSummary>> GetAllProfileDrafts([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new ListUserProfileDraftsQuery(parameters?.CT), cancellationToken);

        if (!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken", response.Item2);
        return response.Item1;
    }
    [HttpGet("Profiles")]
    public async Task<List<ApplicantProfileSummary>> GetAllProfiles([FromQuery]QueryParameters parameters, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new ListUserProfilesQuery(parameters?.CT), cancellationToken);

        if (!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken", response.Item2);
        return response.Item1;
    }
    
    [HttpGet("Profiles/{Id}")]
    public async Task<ApplicantProfile> GetProfile(string Id)
    {
        return await Mediator.Send(new GetUserProfileQuery(Id));
        
    }

    [HttpGet("ProfileDrafts/{Id}")]
    public async Task<ApplicantProfileDraft> GetProfileDraft(string Id)
    {
        return await Mediator.Send(new GetUserProfileDraftQuery(Id));
    }

    [HttpPut("Profiles")]
    public async Task<ApplicantProfile> PutProfileDraft(ApplicantProfile profile, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new PutApplicantProfileCommand(profile));
    }
    
    [HttpPut("ProfileDrafts")]
    public async Task<ApplicantProfileDraft> PutProfile(ApplicantProfileDraft profile, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new PutApplicantProfileDraftCommand(profile));
    }
    [HttpDelete("ProfileDrafts/{Id}")]
    public async Task Delete(string Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public class QueryParameters
    {
        public string? CT { get; set; }
    }
}
