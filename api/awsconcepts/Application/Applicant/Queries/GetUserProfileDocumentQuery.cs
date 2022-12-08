using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Queries
{
    public class GetUserProfileDocumentQuery : IRequest<ApplicantProfileDocumentDetail>
    {
        public GetUserProfileDocumentQuery(string ProfileDocumentId)
        {
            this.ProfileDocumentId = ProfileDocumentId;
        }
        public string ProfileDocumentId { get; }
    }
    public class GetUserProfileDocumentQueryHandler : IRequestHandler<GetUserProfileDocumentQuery, ApplicantProfileDocumentDetail>
    {
        private readonly IEntityRepository<domain.ProfileDocumentDetail> repository;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public GetUserProfileDocumentQueryHandler(IEntityRepository<domain.ProfileDocumentDetail> entityRepository, IIdentity user, IMapper mapper)
        {
            this.repository = entityRepository;
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<ApplicantProfileDocumentDetail> Handle(GetUserProfileDocumentQuery request, CancellationToken cancellationToken)
        {
            domain.ProfileDocumentDetail? result = await repository.Get(request.ProfileDocumentId, request.ProfileDocumentId, cancellationToken);
            return mapper.Map<ApplicantProfileDocumentDetail>(result);
        }
    }
}
