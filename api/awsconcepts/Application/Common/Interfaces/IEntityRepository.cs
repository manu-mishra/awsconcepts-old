namespace Application.Common.Interfaces
{
    public interface IEntityRepository<DomainEntity>
    {
        Task<(List<DomainEntity>, string?)> GetAll(string ScopeId, string? ContinuationToken, CancellationToken CancellationToken);
        Task<DomainEntity?> Get(string EntityId, string ScopeId, CancellationToken CancellationToken);
        Task<bool> Put(DomainEntity DomainEntity, CancellationToken CancellationToken);
        Task<DomainEntity> Delete(DomainEntity DomainEntity, CancellationToken CancellationToken);


        /*  |Entity                         |       pk                     |        sk                      |     
         *  -------------------------------------------------------------------------------------------------
         *  -------------------------------------------------------------------------------------------------
         *  |                               |   Identity Domain            |                                |   
         *  -------------------------------------------------------------------------------------------------
         *  |User                           |       User.Id                |    User.Id                     |
         *  |BrowsingHistory                | BrowsingHistory_User.Id      |    TimeStamp                   |
         *  -------------------------------------------------------------------------------------------------
         *  
         *  
         *  -------------------------------------------------------------------------------------------------
         *  |                                   Applicant Domain
         *  -------------------------------------------------------------------------------------------------
         *  |ApplicantProfileDraft          |       UserProfileDraft.Id    |    ProfileDraft_User.Id        |
         *  |ApplicantProfile               |       UserProfile.Id         |    Profile_User.Id             |
         *  |ApplicantProfileImpressions    |       UserProfile.Id         |    Profile_User.Id             |
         *  |Application                    |       UserProfile.Id         |    Application_User.Id         |
         *  |ApplicationCount               |       UserProfile.Id         |    Application_User.Id         |
         *                                                                                              
         *                                                                                              
         *  -------------------------------------------------------------------------------------------------
         *  |                                  Org Domain                                                |
         *  --------------------------------------------------------------------------------------------------
         *  |Org                            |   Org.Id                     | Org.Id                      |
         *  |JobCount                       |   Org.Id                     | JobCount_Org.Id             |
         *  |Job                            |   Job.Id                     | Job_Org.Id                  |
         *  |Application                    |   Application.Id             | Application_Org.Id          |
         *  |ApplicationCount               |   Job.Id                     | ApplicationCount_Org.Id     |
         *  |JobImpressions                 |   Impressions_Job.Id         | Impressions_Org.Id          |
         *  |JobImpressionCount             |   Impressions_Count_Job.Id   | Impressions_Count_OrgId .Id |
                                            
         *                           
         */

        /*                           
         *  |User                    |     Give me myself            |      T (UserId,UserId)         |        
         *  |User                    |     Give me my Profiles       |     SI (UserId)                |        
         *  |User                    |     Jobs Applied via profile  |      T (ProfileId,*)           |        
         *  |User                    |     My Profile Impressions    |      T (ProfileId,*)           |        
                                                                     
         *  |Org                 |     Give me myself            |      T (OrgId,OrgId)   |        
         *  |Org                 |     Give me myJobs            |     SI (Org.Id,*)          |        
         *  |Org                 |     Give me Applicants By Job |      T (Job.Id,*)              |        
         *  |Org                 |     Give me myJobs            |     SI (Org.Id,*)          |        
         * 
         */
    }
}
