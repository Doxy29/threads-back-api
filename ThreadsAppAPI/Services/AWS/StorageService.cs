using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ThreadsAppAPI.Models.AWS;
using ThreadsAppAPI.Utilities;
using S3Object = ThreadsAppAPI.Models.AWS.S3Object;

namespace ThreadsAppAPI.Services.AWS;

public class StorageService : IStorageService
{
    private readonly IConfiguration _configuration;
    
    public StorageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<S3ResponseDto> UploadFileAsync(S3Object s3obj)
    {
        var response = new S3ResponseDto();
        var decorator = $"{DateTime.UtcNow:yyyyMMddhhmmssfffffffZ}";
        s3obj.Name = $"{decorator}{s3obj.Name}";
        try
        {
            var uploadRequest = new PutObjectRequest()
            {
                InputStream = s3obj.InputStream,
                Key = s3obj.Name,
                BucketName = s3obj.BucketName,
            };

            using var client = new AwsStoreConn(_configuration).GetS3Connection();


            response.ObjResponse = await client.PutObjectAsync(uploadRequest);
            response.StatusCode = 200;
            response.Message = $"{s3obj.Name} has been uploaded!";
        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch(Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
};