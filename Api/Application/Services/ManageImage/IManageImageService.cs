namespace ThreadsBackend.Api.Application.Services;

public interface IManageImageService
{
    Task<string> UploadFile(IFormFile formFile);

    string GetFile(string filename);

    void DeleteImage(string filename);
}
