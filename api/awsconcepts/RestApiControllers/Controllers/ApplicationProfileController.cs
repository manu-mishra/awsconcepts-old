using Application.Applicant.Commands;
using Domain.Applicants;

namespace AwsConceptsRootLambda.Controllers;

public class ApplicationProfileController : ApiControllerBase
{
    [HttpPost()]
    public async Task<ApplicantProfile> AddApplicationProfile(ApplicantProfile profile)
    {
        return await Mediator.Send(new CerateApplicantProfileCommand(profile));
    }
}
