using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Core.DTO;
using OnlineShop.Core.DTO.User;
using OnlineShop.Domain.Entities;


namespace OnlineShop.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            // RegisterDto -> User
            CreateMap<UserRegisterDto, User>();

            // User -> UserDto
            CreateMap<User, UserDto>();

            // ProductDto <-> Product
            CreateMap<ProductDto, Product>().ReverseMap();

            // CreateProductDto -> Product
            CreateMap<CreateProductDto, Product>();

            // UpdateProductDto -> Product
            CreateMap<UpdateProductDto, Product>();
            
            // Item -> ItemDto
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Cost));
            
            // Cart -> CartDto
            CreateMap<Cart, CartDto>();

            // Order -> OrderDto
            CreateMap<Order, OrderDto>();

            // DeliveryUserDto -> DeliveryUser
            CreateMap<DeliveryUserDto, DeliveryUser>().ReverseMap();

            // Comparison -> ComparisonDto
            CreateMap<Comparison, ComparisonDto>();

            // Favorite -> FavoriteDto
            CreateMap<Favorite, FavoriteDto>();

            // IdentityRole -> RoleDto
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
