namespace Application.Common.Interfaces
{
    public interface IEntityRepository<DomainEntity>
    {
        Task<List<DomainEntity>> GetAll(string ScopeId);
        Task<DomainEntity> Get(string ScopeId, string Id);
        Task<DomainEntity> Put(DomainEntity DomainEntity);
        Task Delete(DomainEntity DomainEntity);
    }
}
