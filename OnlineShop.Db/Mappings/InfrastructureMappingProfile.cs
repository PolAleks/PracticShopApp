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
        }
    }
}
