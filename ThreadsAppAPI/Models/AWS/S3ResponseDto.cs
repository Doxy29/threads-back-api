using Amazon.S3.Model;

namespace ThreadsAppAPI.Models.AWS;

public class S3ResponseDto
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "";
    
    public PutObjectResponse? ObjResponse { get; set; }
}