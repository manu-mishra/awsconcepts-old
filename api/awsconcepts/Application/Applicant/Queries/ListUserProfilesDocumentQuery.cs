using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class ListUserProfileDocumentQuery : IRequest<(List<ApplicantProfileDocument>, string?)>
    {

        public ListUserProfileDocumentQuery(string? ContinuationToken = default(string))
        {
            this.ContinuationToken = ContinuationToken;
        }
        public string? ContinuationToken { get; }
    }
    public class ListUserProfileDocumentQueryHandler : IRequestHandler<ListUserProfileDocumentQuery, (List<ApplicantProfileDocument>, string?)>
    {
        private readonly IEntityRepository<domain.ProfileDocument> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public ListUserProfileDocumentQueryHandler(IEntityRepository<domain.ProfileDocument> entityRepository, IIdentity user, IMapper mapper)
        {
            this.repository = entityRepository;
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<(List<ApplicantProfileDocument>, string?)> Handle(ListUserProfileDocumentQuery request, CancellationToken cancellationToken)
        {
            (List<domain.ProfileDocument>, string?) response = await repository.GetAll(user.Id, request.ContinuationToken, cancellationToken);
            return (mapper.Map<List<ApplicantProfileDocument>>(response.Item1), response.Item2);
        }
    }
}
