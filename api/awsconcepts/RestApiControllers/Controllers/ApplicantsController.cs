using Application.Applicant.Commands;
using Application.Applicant.Dto;
using Application.Applicant.Queries;

namespace RestApiControllers.Controllers;

public class ApplicantsController : ApiControllerBase
{
    [HttpGet("ProfileDocuments")]
    public async Task<List<ApplicantProfileDocument>> GetAllProfileDocuments([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        (List<ApplicantProfileDocument>, string) response = await Mediator.Send(new ListUserProfileDocumentQuery(parameters?.ContinuationToken), cancellationToken);

        if (!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken", response.Item2);
        return response.Item1;
    }
    [HttpPost("ProfileDocuments")]
    public async Task<ApplicantProfileDocumentDetail> UploadDocument(IFormFile file, [FromForm] string FileTextContent, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
            throw new ArgumentException("File is required");
        using MemoryStream memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        return await Mediator.Send(new PutProfileDocumentCommand(memoryStream, FileTextContent, file.FileName, file.ContentType), cancellationToken);
    }


    [HttpGet("ProfileDrafts")]
    public async Task<List<ApplicantProfileSummary>> GetAllProfileDrafts([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        (List<ApplicantProfileSummary>, string) response = await Mediator.Send(new ListUserProfileDraftsQuery(parameters?.ContinuationToken), cancellationToken);

        if (!string.IsNullOrEmpty(response.Item2))
            HttpContext.Response.Headers.Add("x-continuationToken", response.Item2);
        return response.Item1;
    }
    [HttpGet("Profiles")]
    public async Task<List<ApplicantProfileSummary>> GetAllProfiles([FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        (List<ApplicantProfileSummary>, string) response = await Mediator.Send(new ListUserProfilesQuery(parameters?.ContinuationToken), cancellationToken);

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
        return await Mediator.Send(new PublishProfileDraftCommand(profile));
    }

    [HttpPut("ProfileDrafts")]
    public async Task<ApplicantProfileDraft> PutProfile(ApplicantProfileDraft profile, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new PutProfileDraftCommand(profile));
    }
    [HttpDelete("ProfileDrafts/{Id}")]
    public async Task Delete(string Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public class QueryParameters
    {
        public string? ContinuationToken { get; set; }
    }
}
