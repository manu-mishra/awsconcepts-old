namespace Application.Common.Interfaces
{
    public interface IEntitySearchProvider
    {
        public List<T> SearchDomainEntity<T>(string SearchString, string scope);
        
    }
}
