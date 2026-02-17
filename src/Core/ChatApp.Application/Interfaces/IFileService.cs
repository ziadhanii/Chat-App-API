namespace ChatApp.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadImageAsync(IFormFile image, string userEmail, CancellationToken cancellationToken = default);
    Task<bool> DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default);
    Task<bool> DeleteUserFolderAsync(string userEmail, CancellationToken cancellationToken = default);
}