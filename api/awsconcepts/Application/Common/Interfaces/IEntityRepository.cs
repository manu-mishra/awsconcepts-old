namespace Application.Common.Interfaces
{
    public interface IEntityRepository<DomainEntity>
    {
        Task<List<DomainEntity>> GetAll(string ScopeId);
        Task<DomainEntity?> Get(string ScopeId, string Id);
        Task<bool> Put(DomainEntity DomainEntity);
        Task<bool> Delete(DomainEntity DomainEntity);
    }
}
