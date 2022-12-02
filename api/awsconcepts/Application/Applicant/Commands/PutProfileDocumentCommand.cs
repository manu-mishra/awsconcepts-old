using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using Domain.Applicants;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class PutProfileDocumentCommand : IRequest<ApplicantProfileDocumentDetail>
    {
        public PutProfileDocumentCommand(
            Stream document,
            string DocumentText,
            string DocumentName,
            string ContentType)
        {
            Document = document;
            this.DocumentText = DocumentText;
            this.DocumentName = DocumentName;
            this.ContentType = ContentType;
        }

        public Stream Document { get; }
        public string DocumentText { get; }
        public string DocumentName { get; }
        public string ContentType { get; }
    }

    public class PutProfileDocumentCommandHandeller : IRequestHandler<PutProfileDocumentCommand, ApplicantProfileDocumentDetail>
    {
        private readonly IEntityRepository<domain.ProfileDocument> profileDocumentRepo;
        private readonly IEntityRepository<domain.ProfileDocumentDetail> profileDocumentDetailsRepo;
        private readonly IFileStorageRepository storageRepository;
        private readonly ITextAnalysisProvider textAnalysisProvider;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public PutProfileDocumentCommandHandeller(
            IEntityRepository<domain.ProfileDocument> ProfileDocumenRepository,
            IEntityRepository<domain.ProfileDocumentDetail> ProfileDocumenDetailsRepository,
            IFileStorageRepository StorageRepository,
            ITextAnalysisProvider TextAnalysisProvider,
            IIdentity User, IMapper Mapper)
        {
            profileDocumentRepo = ProfileDocumenRepository;
            profileDocumentDetailsRepo = ProfileDocumenDetailsRepository;
            mapper = Mapper;
            storageRepository = StorageRepository;
            textAnalysisProvider = TextAnalysisProvider;
            user = User;
        }


        public async Task<ApplicantProfileDocumentDetail> Handle(PutProfileDocumentCommand request, CancellationToken cancellationToken)
        {
            string id = Guid.NewGuid().ToString();
            string fileSize = request.Document.Length.ToString();
            List<Domain.ValueTypes.TextAnalysis> analysis = await textAnalysisProvider.AnalyseText(request.DocumentText, cancellationToken);
            List<Domain.ValueTypes.TextAnalysis> piiAnalysis = await textAnalysisProvider.AnalysePii(request.DocumentText, cancellationToken);
            analysis.AddRange(piiAnalysis);
            ProfileDocument document = new ProfileDocument { Id = id, IdentityId = user.Id, Name = request.DocumentName, ContentType = request.ContentType };
            ProfileDocumentDetail documentDetail = new ProfileDocumentDetail { Id = id, IdentityId=user.Id, Analysis = analysis, Name = request.DocumentName, DocumentText = request.DocumentText, Size = fileSize };
            await storageRepository.PutFile(request.Document, id, request.ContentType, cancellationToken);
            await profileDocumentRepo.Put(document, cancellationToken);
            await profileDocumentDetailsRepo.Put(documentDetail, cancellationToken);
            return mapper.Map<ApplicantProfileDocumentDetail>(documentDetail);
        }
    }
}
