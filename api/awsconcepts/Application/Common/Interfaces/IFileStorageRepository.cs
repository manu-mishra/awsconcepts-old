namespace Application.Common.Interfaces
{
    public interface IFileStorageRepository
    {
        Task PutFile(Stream File, string FileKey, string ContentType);
        Task<Tuple<Stream, string>> GetFile(string FileKey);
    }
}
