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

            // ProductDto -> ProductViewModel
            CreateMap<ProductDto, ProductViewModel>();

            // CreateProductViewModel -> CreateProductDto
            CreateMap<CreateProductViewModel, CreateProductDto>();

            // ProductDto -> UpdateProductViewModel
            CreateMap<ProductDto, UpdateProductViewModel>();

            // UpdateProductViewModel -> UpdateProductDto
            CreateMap<UpdateProductViewModel, UpdateProductDto>();

            // ItemDto -> ItemViewModel
            CreateMap<ItemDto, ItemViewModel>()
               .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => (src.ProductPrice * src.Quantity)));

            // CartDto -> CartViewModel
            CreateMap<CartDto, CartViewModel>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Items.Sum(i => i.ProductPrice * i.Quantity)))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity)));

            // OrderDto -> OrderViewModel
            CreateMap<OrderDto, OrderViewModel>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDateTime.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationDateTime.ToString("HH:mm")));

            // OrderViewModel - OrderDto
            CreateMap<OrderViewModel, OrderDto>();

            // DeliveryUserViewModel <-> DeliveryUserDto
            CreateMap<DeliveryUserViewModel, DeliveryUserDto>().ReverseMap();

            // ComparisonDto -> ComparisonViewModel
            CreateMap<ComparisonDto, ComparisonViewModel>();

            // FavoriteDto -> FavoriteViewModel
            CreateMap<FavoriteDto, FavoriteViewModel>();
        }
    }
}
