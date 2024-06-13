using ThreadsAppAPI.Models.AWS;

namespace ThreadsAppAPI.Services.AWS;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3Object s3obj);
}