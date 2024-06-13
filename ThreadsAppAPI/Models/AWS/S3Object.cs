namespace ThreadsAppAPI.Models.AWS;

public class S3Object
{
    public string Name { get; set; } = null;
    public Stream InputStream { get; set; } = null;
    public string BucketName { get; set; } = null;
}