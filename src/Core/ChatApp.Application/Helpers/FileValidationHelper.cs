namespace ChatApp.Application.Helpers;

public static class FileValidationHelper
{
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxImageSizeInBytes = 5 * 1024 * 1024;

    private static bool IsValidImageExtension(IFormFile? file)
    {
        if (file is null) return false;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return AllowedImageExtensions.Contains(extension);
    }

    private static bool IsValidImageSize(IFormFile? file)
    {
        if (file is null) return false;

        return file.Length is > 0 and <= MaxImageSizeInBytes;
    }

    public static bool IsValidImage(IFormFile? file)
    {
        return IsValidImageExtension(file) && IsValidImageSize(file);
    }
}