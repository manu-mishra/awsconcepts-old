using Application.Applicant.Commands;
using Application.Applicant.Dto;

namespace RestApiControllers.Controllers;

public class ApplicantsController : ApiControllerBase
{
    [HttpGet("ProfileDrafts")]
    public  string GetAllProfileDrafts(QueryParameters parameters, CancellationToken cancellationToken)
    {
        return "Hi";
    }
    [HttpGet("Profiles")]
    public async Task<ApplicantProfile> GetAllProfiles(QueryParameters parameters, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("Profiles/{Id}")]
    public async Task<ApplicantProfileDraft> GetProfile(string Id)
    {
        return await Mediator.Send(new CerateApplicantProfileDraftCommand(new ApplicantProfileDraft(Id, "","","")));
        
    }

    [HttpGet("ProfileDrafts/{Id}")]
    public string GetProfileDraft(string Id)
    {
        return $"Hi{Id}";
    }

    [HttpPut("ProfileDrafts")]
    public async Task<ApplicantProfile> Put(ApplicantProfile profile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
