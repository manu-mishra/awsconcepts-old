using Application.Common.Interfaces;
using Application.Identity;
using AutoMapper;
using domain = Domain.Applicants;

namespace Application.Applicant.Commands
{
    public class DeleteProfileDocumentCommand : IRequest<bool>
    {
        public DeleteProfileDocumentCommand(string Id)
        {
            this.Id = Id;
        }

        public string Id { get; }
    }

    public class DeleteProfileDocumentCommandHandeller : IRequestHandler<DeleteProfileDocumentCommand, bool>
    {
        private readonly IEntityRepository<domain.ProfileDocument> profileDocumentRepo;
        private readonly IEntityRepository<domain.ProfileDocumentDetail> profileDocumentDetailsRepo;
        private readonly IFileStorageRepository storageRepository;
        private readonly ITextAnalysisProvider textAnalysisProvider;
        private readonly IIdentity user;
        private readonly IMapper mapper;

        public DeleteProfileDocumentCommandHandeller(
            IEntityRepository<domain.ProfileDocument> ProfileDocumenRepository,
            IEntityRepository<domain.ProfileDocumentDetail> ProfileDocumenDetailsRepository,
            IFileStorageRepository StorageRepository,
            IIdentity User)
        {
            profileDocumentRepo = ProfileDocumenRepository;
            profileDocumentDetailsRepo = ProfileDocumenDetailsRepository;
            storageRepository = StorageRepository;
            user = User;
        }


        public async Task<bool> Handle(DeleteProfileDocumentCommand request, CancellationToken cancellationToken)
        {
            await profileDocumentRepo.Delete(request.Id, user.Id, cancellationToken);
            await profileDocumentDetailsRepo.Delete(request.Id, request.Id, cancellationToken);
            return true;
        }
    }
}
