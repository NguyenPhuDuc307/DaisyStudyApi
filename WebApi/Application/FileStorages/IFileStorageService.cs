using Microsoft.AspNetCore.Http;

namespace Application.FileStorages
{
    public interface IFileStorageService
    {
        Task<string> SaveFile(IFormFile file);
        string GetFileUrl(string fileName);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}