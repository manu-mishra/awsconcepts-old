namespace Application.Common.Interfaces
{
    public interface IEntitySearchProvider
    {
        public Task<List<T>> SearchInScopeDomainEntity<T>(string SearchString, string scope) where T : class;
        public Task<List<T>> SearchWithNoScopeDomainEntity<T>(string SearchString) where T : class;

    }
}
