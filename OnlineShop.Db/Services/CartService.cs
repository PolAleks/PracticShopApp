using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Exceptions;

namespace OnlineShop.Infrastructure.Services
{
    public class CartService(DatabaseContext context,
                             IMapper mapper) : ICartService
    {
        private readonly DatabaseContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var cartDto = _mapper.Map<CartDto>(cart);

            return cartDto;
        }

        public async Task AddToCartAsync(string userId, int productId)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Item item = new()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1                    
                };

                _context.Items.Add(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task IncreaseQuantityAsync(string userId, int productId)
        {
            var existingItem = await _context.Items
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Cart!.UserId == userId && ci.ProductId == productId);

            if (existingItem == null)
            {
                throw new NotFoundException("Данный товар не найден в корзине");
            }

            existingItem.Quantity++;

            await _context.SaveChangesAsync();
        }

        public async Task DecreaseQuantityAsync(string userId, int productId)
        {
            var exisringItem = await _context.Items
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Cart!.UserId == userId && ci.ProductId == productId);

            if (exisringItem == null)
            {
                throw new NotFoundException("Данный товар не найден в корзине");
            }

            if (exisringItem.Quantity == 1)
            {
                _context.Items.Remove(exisringItem);
            }
            else
            {
                exisringItem.Quantity--;
            }
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);

            if(cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    Items = []
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
