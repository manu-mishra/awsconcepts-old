using Application.Applicant.Dto;
using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using Domain.Applicants;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class PutApplicantProfileDocumentCommand : IRequest<ApplicantProfileDocumentDetail>
    {
        public PutApplicantProfileDocumentCommand(
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

    public class PutApplicantProfileDocumentCommandHandeller : IRequestHandler<PutApplicantProfileDocumentCommand, ApplicantProfileDocumentDetail>
    {
        private readonly IEntityRepository<domain.ProfileDocument> profileDocumentRepo;
        private readonly IEntityRepository<domain.ProfileDocumentDetail> profileDocumentDetailsRepo;
        private readonly IFileStorageRepository storageRepository;
        private readonly ITextAnalysisProvider textAnalysisProvider;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public PutApplicantProfileDocumentCommandHandeller(
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


        public async Task<ApplicantProfileDocumentDetail> Handle(PutApplicantProfileDocumentCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString();
            var fileSize = request.Document.Length.ToString();
            var analysis = await textAnalysisProvider.AnalyseText(request.DocumentText, cancellationToken);
            var piiAnalysis = await textAnalysisProvider.AnalysePii(request.DocumentText, cancellationToken);
            analysis.AddRange(piiAnalysis);
            var document = new ProfileDocument { Id = id, IdentityId =user.Id, Name = request.DocumentName, ContentType = request.ContentType };
            var documentDetail = new ProfileDocumentDetail { Id = id, Analysis= analysis,Name= request.DocumentName, DocumentText = request.DocumentText, Size = fileSize };
            await storageRepository.PutFile(request.Document, id, request.ContentType, cancellationToken);
            await profileDocumentRepo.Put(document, cancellationToken);
            await profileDocumentDetailsRepo.Put(documentDetail, cancellationToken);
            return mapper.Map<ApplicantProfileDocumentDetail>(documentDetail);
        }
    }
}
