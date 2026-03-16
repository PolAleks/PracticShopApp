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
            CreateMap<UpdateProductViewModel, UpdateProductDto>();
            #endregion

            #region Item for Cart & Order
            // ItemDto -> ItemViewModel
            CreateMap<ItemDto, ItemViewModel>()
               .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => (src.ProductPrice * src.Quantity)));
            #endregion

            #region Cart
            // CartDto -> CartViewModel
            CreateMap<CartDto, CartViewModel>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Items.Sum(i => i.ProductPrice * i.Quantity)))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity)));
            #endregion

            #region Order
            // OrderDto -> OrderViewModel
            CreateMap<OrderDto,  OrderViewModel>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Items.Sum(oi => oi.ProductPrice * oi.Quantity)))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Items.Sum(oi => oi.Quantity)))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDateTime.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationDateTime.ToString("HH:mm")));

            // CreationOrderViewModel
            #endregion
        }
    }
}
