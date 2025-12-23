using Amazon.S3.Model;
using Amazon.S3;
using System;
using tms.Utilities;
using System.Threading.Tasks;

namespace tms.Services
{
    public class S3Service
    {
        private readonly AmazonS3Client _s3Client;
        public S3Service(AmazonS3ClientFactory amazonS3ClientFactory)
        {
            _s3Client = amazonS3ClientFactory.Get();
        }
        public async Task<string> GeneratePreSignedUrl(string bucketName, string path, HttpVerb httpVerb = HttpVerb.PUT)
        {
            var s3request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = path,
                Verb = httpVerb,
                Expires = DateTime.Now.AddMinutes(10),

            };

            return await _s3Client.GetPreSignedURLAsync(s3request);
        }
    }
}
