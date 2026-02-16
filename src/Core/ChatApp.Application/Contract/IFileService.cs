using Microsoft.AspNetCore.Http;

namespace ChatApp.Application.Contract;

public interface IFileService
{
    Task<string> SaveImageAsync(IFormFile file, string email);
    Task<bool> DeleteImageAsync(string path);
}