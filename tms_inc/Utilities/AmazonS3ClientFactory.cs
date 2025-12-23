using Amazon.Runtime;
using Amazon.S3;
using tms.AppConfiguration;

namespace tms.Utilities
{
    public class AmazonS3ClientFactory
    {
        private AwsConfiguration _config;
        public AmazonS3ClientFactory(AwsConfiguration config)
        {
            _config = config;
        }

        public AmazonS3Client Get()
        {
#if DEBUG
            return new AmazonS3Client(new BasicAWSCredentials(_config.AccessKey, _config.SecretKey), Amazon.RegionEndpoint.USEast2);
#else
                 return new Amazon.S3.AmazonS3Client();
#endif
        }
    }
}
