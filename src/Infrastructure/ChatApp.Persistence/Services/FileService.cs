namespace ChatApp.Persistence.Services;

public class FileService(IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor) : IFileService
{
    private readonly string _imagesPath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "images");

    public async Task<string> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_imagesPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

        var filePath = Path.Combine(_imagesPath, fileName);

        await using var stream = File.Create(filePath);

        await image.CopyToAsync(stream, cancellationToken);

        var request = httpContextAccessor.HttpContext?.Request;

        var baseUrl = $"{request?.Scheme}://{request?.Host}";

        return $"{baseUrl}/images/{fileName}";
    }

    public Task<bool> DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return Task.FromResult(false);

            var uri = new Uri(imageUrl);

            var fileName = Path.GetFileName(uri.LocalPath);

            var filePath = Path.Combine(_imagesPath, fileName);

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
}