using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile image, string folder, string fileName);
        void DeleteImage(string imagePath);
    }
}
