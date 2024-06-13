using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.Util;

namespace ThreadsAppAPI.Utilities;

public class AwsStoreConn
{
    private readonly IConfiguration _config;

    public AwsStoreConn(IConfiguration config)
    {
        _config = config;
    }

    public AmazonS3Client GetS3Connection()
    {
        
        var s3Config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUCentral1
        };
        
        var credentials = new BasicAWSCredentials(
            _config["AwsCredentials:AccessKey"], 
            _config["AwsCredentials:SecretKey"]
        );
        
        return new AmazonS3Client(credentials,s3Config);
    }
}