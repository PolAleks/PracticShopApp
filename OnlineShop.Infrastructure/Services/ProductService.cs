using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Exceptions;
using System.Data;

namespace OnlineShop.Infrastructure.Services
{
    public class ProductService(DatabaseContext context,
                                IMapper mapper,
                                IImageService imageService) : IProductService
    {
        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            var product = context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                // Удаление изображений товара с сервера
                foreach (var image in product.ProductImages)
                {
                    imageService.DeleteImage(image.ImagePath);
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await context.Products
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productDto = await context.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider) //Маппинг на уровне БД
                .FirstOrDefaultAsync();

            if (productDto == null)
            {
                throw new NotFoundException($"Товар с идентификатором: {id} не найден");
            }

            return productDto;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var product = mapper.Map<Product>(createProductDto);
                product.ProductImages = new HashSet<ProductImage>();

                context.Products.Add(product);
                await context.SaveChangesAsync();

                if (createProductDto.Images != null && createProductDto.Images.Any())
                {
                    var images = await imageService.SaveProductImagesAsync(createProductDto.Images, product.Id);

                    var productImages = images.Select((path, index) => new ProductImage()
                    {
                        ProductId = product.Id,
                        ImagePath = path,
                        IsMain = index == 0
                    });

                    await context.ProductImages.AddRangeAsync(productImages);

                    product.PhotoPath = productImages.First(i => i.IsMain).ImagePath;

                    await context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return mapper.Map<ProductDto>(product);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string? query)
        {
            if (string.IsNullOrEmpty(query))
                return await GetAllProductsAsync();

            return await context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{query}%") ||
                            EF.Functions.Like(p.Description, $"%{query}%"))
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var product = await context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(p => p.Id == updateProductDto.Id)
                    ?? throw new Exception($"Товар с идентификатором: {updateProductDto.Id} не найден");

                // Обновляем основные поля
                mapper.Map(updateProductDto, product);

                // Удаляем отмеченные изображения
                if (updateProductDto.ImagesToDelete != null && updateProductDto.ImagesToDelete.Any())
                {
                    var imagesToDelete = product.ProductImages
                        .Where(img => updateProductDto.ImagesToDelete.Contains(img.Id));

                    foreach (var image in imagesToDelete)
                    {
                        imageService.DeleteImage(image.ImagePath);

                        context.ProductImages.Remove(image);
                    }
                }

                // Добавляем новое изображение
                if (updateProductDto.NewImages != null && updateProductDto.NewImages.Any())
                {
                    var newImagesPath = await imageService.SaveProductImagesAsync(updateProductDto.NewImages, updateProductDto.Id);

                    var newProductImages = newImagesPath.Select((path, index) => new ProductImage()
                    {
                        ProductId = product.Id,
                        ImagePath = path,
                        IsMain = !product.ProductImages.Any() && index == 0
                    });

                    await context.ProductImages.AddRangeAsync(newProductImages);
                }
                await context.SaveChangesAsync();

                // Обновляем PhotoPath для обратной совместимости
                var mainImage = product.ProductImages.FirstOrDefault(i => i.IsMain);
                if (mainImage != null)
                {
                    product.PhotoPath = mainImage.ImagePath;
                }
                else if (product.ProductImages.Any())
                {
                    // Если нет основного изображения, делаем первое основным
                    var firstImage = product.ProductImages.First();
                    firstImage.IsMain = true;
                    product.PhotoPath = firstImage.ImagePath;

                    // Обновляем в базе данных
                    context.ProductImages.Update(firstImage);
                }
                else
                {
                    product.PhotoPath = null;
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return mapper.Map<ProductDto>(product);

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ProductDto> GetProductWithImagesByIdAsync(int id)
        {
            var product = await context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException($"Товар с идентификатором: {id} не найден");

            var productDto = mapper.Map<ProductDto>(product);

            return productDto;
        }
    }
}
