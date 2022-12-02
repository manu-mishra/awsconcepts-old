namespace Infrastructure.Repository.Config
{
    internal class EntityConfigLookUp
    {
        EntityConfigLookUp()
        {
            RepoConfig = new Dictionary<Type, EntityConfig>();
            Configure();
        }
        public Dictionary<Type, EntityConfig> RepoConfig { get; private set; }
        internal static EntityConfigLookUp GetConfigMap()
        {
            EntityConfigLookUp config = new EntityConfigLookUp();
            return config;
        }

        private void Configure()
        {
            // Applicant Domain
            RepoConfig.Add(typeof(Domain.Applicants.ProfileDraft),
                new EntityConfig("Id", "IdentityId", pkPrefix: "A_PD#", skPrefix: "A_PD-U#"));
            RepoConfig.Add(typeof(Domain.Applicants.Profile),
                new EntityConfig("Id", "IdentityId", pkPrefix: "A_P#", skPrefix: "A_P-U#"));
            RepoConfig.Add(typeof(Domain.Applicants.Application),
                new EntityConfig("Id", "IdentityId", pkPrefix: "A_A#", skPrefix: "A_A-A_P#"));
            RepoConfig.Add(typeof(Domain.Applicants.ProfileDocument),
                new EntityConfig("Id", "IdentityId", pkPrefix: "A_PDF#", skPrefix: "A_A-U#"));
            RepoConfig.Add(typeof(Domain.Applicants.ProfileDocumentDetail),
                new EntityConfig("Id", "Id", pkPrefix: "A_PDFD#", skPrefix: "A_PDFD#"));

            // Company Domain
            RepoConfig.Add(typeof(Domain.Organizations.Organization),
                new EntityConfig("Id", "IdentityId", pkPrefix: "O_O#", skPrefix: "O_O-U#"));
            RepoConfig.Add(typeof(Domain.Organizations.Job),
                new EntityConfig("Id", "OrganizationId", pkPrefix: "O_J#", skPrefix: "O_J-O_O#"));
            RepoConfig.Add(typeof(Domain.Organizations.Application),
                new EntityConfig("Id", "JobId", pkPrefix: "O_A#", skPrefix: "O_A-O_J#"));
        }
    }


}
