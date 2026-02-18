namespace ChatApp.Persistence.Services;

public class FileService(IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor) : IFileService
{
    private readonly string _baseImagesPath =
        Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "images", "users");

    public async Task<string> UploadImageAsync(IFormFile image, string userEmail,
        CancellationToken cancellationToken = default)
    {
        var sanitizedEmail = SanitizeFileName(userEmail);
        var userFolderPath = Path.Combine(_baseImagesPath, sanitizedEmail);

        Directory.CreateDirectory(userFolderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var filePath = Path.Combine(userFolderPath, fileName);

        await using var stream = File.Create(filePath);

        await image.CopyToAsync(stream, cancellationToken);

        var request = httpContextAccessor.HttpContext?.Request;
        var baseUrl = $"{request?.Scheme}://{request?.Host}";

        return $"{baseUrl}/images/users/{sanitizedEmail}/{fileName}";
    }

    public Task<bool> DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return Task.FromResult(false);

            var uri = new Uri(imageUrl);
            var localPath = uri.LocalPath;

            var imagesIndex = localPath.IndexOf("/images/", StringComparison.OrdinalIgnoreCase);
            if (imagesIndex == -1)
                return Task.FromResult(false);

            var relativePath = localPath[(imagesIndex + "/images/".Length)..];
            var filePath = Path.Combine(
                hostEnvironment.ContentRootPath,
                "wwwroot",
                "images",
                relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> DeleteUserFolderAsync(string userEmail, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                return Task.FromResult(false);

            var sanitizedEmail = SanitizeFileName(userEmail);
            var userFolderPath = Path.Combine(_baseImagesPath, sanitizedEmail);

            if (Directory.Exists(userFolderPath))
            {
                Directory.Delete(userFolderPath, recursive: true);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();

        var sanitized = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

        sanitized = sanitized.Replace("@", "_at_");

        return sanitized.ToLowerInvariant();
    }
}