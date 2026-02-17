namespace ChatApp.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default);
    Task<bool> DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default);
}