using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OnlineShop.Core.Interfaces.Services;

namespace OnlineShop.Infrastructure.Services
{
    public class ImageService(IWebHostEnvironment environment) : IImageService
    {
        private readonly string[] _allowedExtentions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const int MaxFileSize = 5 * 1024 * 1024;

        public async Task<string> SaveImageAsync(IFormFile file, string folder, string fileName)
        {
            if (!IsValidImage(file))
                throw new ArgumentException("Недопустимый файл изображения");

            // Создание каталога в котором будет хранится файл
            var uploadPath = Path.Combine(environment.WebRootPath, "images", folder);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var extention = Path.GetExtension(file.FileName).ToLowerInvariant();

            // Сохранение файла - абсолютный путь
            var pathFile = Path.Combine(uploadPath, fileName + extention);

            using var stream = new FileStream(pathFile, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/{folder}/{fileName}{extention}";
        }

        private bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > MaxFileSize)
                return false;

            var extention = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            return _allowedExtentions.Contains(extention);
        }
    }
}
