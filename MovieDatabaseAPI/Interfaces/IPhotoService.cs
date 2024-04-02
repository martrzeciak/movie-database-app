using CloudinaryDotNet.Actions;

namespace MovieDatabaseAPI.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> AddImageAsync(IFormFile file, string option);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
