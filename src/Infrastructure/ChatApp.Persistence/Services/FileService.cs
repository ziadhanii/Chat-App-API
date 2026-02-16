namespace ChatApp.Persistence.Services;

public class FileService : IFileService
{
    private readonly string _rootPath;

    public FileService()
    {
        _rootPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "images");

        Directory.CreateDirectory(_rootPath);
    }

    public async Task<string> SaveImageAsync(IFormFile file, string email)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
            throw new Exception("Invalid image format");

        var safeEmail = email.Replace("@", "_").Replace(".", "_");

        var fileName = $"{safeEmail}_{Guid.NewGuid()}{extension}";

        var fullPath = Path.Combine(_rootPath, fileName);

        await using var stream = new FileStream(
            fullPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None);

        await file.CopyToAsync(stream);

        return Path.Combine("images", fileName)
            .Replace("\\", "/");
    }

    public async Task<bool> DeleteImageAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        var fullPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            path);

        if (!File.Exists(fullPath))
            return false;

        await Task.Run(() => File.Delete(fullPath));

        return true;
    }
}