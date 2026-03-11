using AutoMapper;
using OnlineShop.Core.DTO.User;
using OnlineShop.Domain.Entities;


namespace OnlineShop.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            // RegisterDto -> User
            CreateMap<RegisterUserDto, User>();

            // User -> UserDto
            CreateMap<User, UserDto>();
        }
    }
}
