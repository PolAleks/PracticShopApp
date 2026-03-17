using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTO;
using OnlineShop.Core.Interfaces.Services;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Data;

namespace OnlineShop.Infrastructure.Services
{
    public class OrderService(DatabaseContext context,
                              IMapper mapper) : IOrderService
    {
        public async Task CreateOrderAsync(CreateOrderDto orderDto)
        {
            var cart = await context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == orderDto.UserId);

            if (cart == null || !cart.Items.Any())
            {
                throw new Exception("В корзине отсутствуют товары");
            }

            Order order = new()
            {
                UserId = orderDto.UserId,
                DeliveryUser = mapper.Map<DeliveryUser>(orderDto.DeliveryUser),
                CreationDateTime = DateTime.Now,
                Status = OrderStatus.Created,
                Items = cart.Items
            };

            await context.Orders.AddAsync(order);

            context.Carts.Remove(cart);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync()
        {
            var order = await context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.CreationDateTime)
                .ToListAsync();

            var orderDto = mapper.Map<IEnumerable<OrderDto>>(order);

            return orderDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            var order = await context.Orders
                .Include(o => o.DeliveryUser)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            var orderDto = mapper.Map<OrderDto>(order);

            return orderDto;
        }

        public async Task UpdateStatusAsync(Guid id, OrderStatus status)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order != null)
            {
                order.Status = status;
                await context.SaveChangesAsync();
            }
        }
    }
}
