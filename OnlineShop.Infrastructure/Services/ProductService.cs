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
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
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

                context.Products.Add(product);
                await context.SaveChangesAsync();

                if (createProductDto.Image != null)
                {
                    string fileName = GetFileName(product);

                    var imagePath = await imageService.SaveImageAsync(createProductDto.Image, "product", fileName);

                    product.PhotoPath = imagePath;
                }
                else
                {
                    product.PhotoPath = "/img/product.png";
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
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == updateProductDto.Id)
                ?? throw new Exception($"Товар с идентификатором: {updateProductDto.Id} не найден");

            mapper.Map(updateProductDto, product);

            if(updateProductDto.Image != null)
            {
                if (!string.IsNullOrEmpty(product.PhotoPath))
                {
                    imageService.DeleteImage(product.PhotoPath);
                }

                var fileName = GetFileName(product);

                product.PhotoPath = await imageService.SaveImageAsync(updateProductDto.Image, "product", fileName);
            }

            await context.SaveChangesAsync();

            return mapper.Map<ProductDto>(product);
        }

        private static string GetFileName(Product product)
        {
            return $"{product.Id}_{Guid.NewGuid():N}";
        }

    }
}
