namespace Infrastructure.Repository.Config
{
    internal class RepositoryConfigLookUp
    {
        RepositoryConfigLookUp()
        {
            RepoConfig = new Dictionary<Type, RepositoryConfig>();
            Configure();
        }
        public Dictionary<Type, RepositoryConfig> RepoConfig { get; private set; }
        internal static RepositoryConfigLookUp GetConfigMap()
        {
            RepositoryConfigLookUp config = new RepositoryConfigLookUp();
            return config;
        }

        private void Configure()
        {
            // Applicant Domain
            RepoConfig.Add(typeof(Domain.Applicants.ProfileDraft),
                new RepositoryConfig("Id", "IdentityId", pkPrefix: "A_PD#", skPrefix: "A_PD-U#"));
            RepoConfig.Add(typeof(Domain.Applicants.Profile),
                new RepositoryConfig("Id", "IdentityId", pkPrefix: "A_P#", skPrefix: "A_P-U#"));
            RepoConfig.Add(typeof(Domain.Applicants.Application),
                new RepositoryConfig("Id", "IdentityId", pkPrefix: "A_A#", skPrefix: "A_A-A_P#"));
            RepoConfig.Add(typeof(Domain.Applicants.ProfileDocument),
                new RepositoryConfig("Id", "IdentityId", pkPrefix: "A_PDF#", skPrefix: "A_A-U#"));
            RepoConfig.Add(typeof(Domain.Applicants.ProfileDocumentDetail),
                new RepositoryConfig("Id", "Id", pkPrefix: "A_PDFD#", skPrefix: "A_PDFD#"));

            // Company Domain
            RepoConfig.Add(typeof(Domain.Organizations.Organization),
                new RepositoryConfig("Id", "IdentityId", pkPrefix: "O_O#", skPrefix: "O_O-U#"));
            RepoConfig.Add(typeof(Domain.Organizations.Job),
                new RepositoryConfig("Id", "OrganizationId", pkPrefix: "O_J#", skPrefix: "O_J-O_O#"));
            RepoConfig.Add(typeof(Domain.Organizations.Application),
                new RepositoryConfig("Id", "JobId", pkPrefix: "O_A#", skPrefix: "O_A-O_J#"));
        }
    }


}
