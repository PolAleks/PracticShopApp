using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Exceptions;

namespace OnlineShop.Infrastructure.Services
{
    public class ProductService(DatabaseContext context, IMapper mapper) : IProductService
    {
        private readonly DatabaseContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productDto = await _context.Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider) //Маппинг на уровне БД
                .FirstOrDefaultAsync();

            if (productDto == null)
            {
                throw new NotFoundException($"Товар с идентификатором: {id} не найден");
            }

            return productDto;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string? query)
        {
            if (string.IsNullOrEmpty(query))
                return await GetAllProductsAsync();

            return await _context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{query}%") || 
                            EF.Functions.Like(p.Description, $"%{query}%"))
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == updateProductDto.Id)
                ?? throw new Exception($"Товар и идентификатором: {updateProductDto.Id} не найден");

            _mapper.Map(updateProductDto, product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
    }
}
