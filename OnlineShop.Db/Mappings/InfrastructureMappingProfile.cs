using AutoMapper;
using OnlineShop.Core.DTO;
using OnlineShop.Core.DTO.User;
using OnlineShop.Domain.Entities;


namespace OnlineShop.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            #region User
            // RegisterDto -> User
            CreateMap<RegisterUserDto, User>();

            // User -> UserDto
            CreateMap<User, UserDto>();
            #endregion

            #region Product
            // ProductDto <-> Product
            CreateMap<ProductDto, Product>().ReverseMap();

            // CreateProductDto -> Product
            CreateMap<CreateProductDto, Product>();

            // UpdateProductDto -> Product
            CreateMap<UpdateProductDto, Product>();
            #endregion

            #region Item for Cart & Order
            // Item -> ItemDto
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Cost));
            #endregion

            #region Cart
            // Cart -> CartDto
            CreateMap<Cart, CartDto>();
            #endregion

            #region Order
            // Order -> OrderDto
            CreateMap<Order, OrderDto>();
            #endregion

            // DeliveryUserDto -> DeliveryUser
            CreateMap<DeliveryUserDto, DeliveryUser>().ReverseMap();
        }
    }
}
