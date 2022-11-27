using Amazon.S3;
using Amazon.S3.Model;
using Application.Common.Interfaces;

namespace Infrastructure.Storage
{
    public class S3StorageRepository : IFileStorageRepository
    {
        private readonly IAmazonS3 s3Client;
        const string bucketname = "awsconcepts";
        public S3StorageRepository(IAmazonS3 S3Client)
        {
            s3Client = S3Client;
        }
        public async Task<Tuple<Stream, string>> GetFile(string FileKey)
        {
            var s3Object =await s3Client.GetObjectAsync(bucketname, FileKey);
            return new Tuple<Stream, string>(s3Object.ResponseStream, s3Object.Headers.ContentType);
        }

        public async Task PutFile(Stream File, string FileKey, string ContentType)
        {
            var request = new PutObjectRequest()
            {
                BucketName = bucketname,
                Key = FileKey,
                InputStream = File
            };
            request.Metadata.Add("Content-Type", ContentType);
            await s3Client.PutObjectAsync(request);
        }
    }
}
