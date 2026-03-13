using AutoMapper;
using OnlineShop.Core.DTO;
using OnlineShop.Core.DTO.User;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Mappings
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            #region User
            // UserDto -> UserViewModel
            CreateMap<UserDto, UserViewModel>();

            // RegisterViewModel -> RegisterDto
            CreateMap<RegisterViewModel, RegisterUserDto>()
                .ForMember(u => u.Email, opt => opt.MapFrom(dto => dto.FirstName));

            // UserDto -> EditUserViewModel
            CreateMap<UserDto, EditUserViewModel>();

            // EditUserViewModel -> UpdateUserDto
            CreateMap<EditUserViewModel, UpdateUserDto>();

            // ChangeUserPasswordViewModel -> ChangeUserPasswordDto
            CreateMap<ChangeUserPasswordViewModel, ChangeUserPasswordDto>();

            // ChangaRoleViewModel -> ChangeUserRoleDto
            CreateMap<ChangeRoleViewModel, ChangeUserRoleDto>();
            #endregion

            #region Product
            // ProductDto -> ProductViewModel
            CreateMap<ProductDto, ProductViewModel>();

            // CreateProductViewModel -> CreateProductDto
            CreateMap<CreateProductViewModel, CreateProductDto>();

            // ProductDto -> UpdateProductViewModel
            CreateMap<ProductDto, UpdateProductViewModel>();

            // UpdateProductViewModel -> UpdateProductDto
            CreateMap<UpdateProductViewModel,  UpdateProductDto>();
            #endregion
        }
    }
}
