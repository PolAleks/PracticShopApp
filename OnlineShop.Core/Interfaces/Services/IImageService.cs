using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file, string fileName, string folder);
        Task<IEnumerable<string>> SaveProductImagesAsync(IEnumerable<IFormFile> files, int productId);
        void DeleteImage(string imagePath);
    }
}
