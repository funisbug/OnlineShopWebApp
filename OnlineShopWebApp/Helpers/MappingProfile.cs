using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Cart, CartViewModel>();
            CreateMap<CartItem, CartItemViewModel>();
            CreateMap<Favorite, FavoriteViewModel>();
            CreateMap<Comparison, ComparisonViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderData, OrderDataViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>();
            CreateMap<IdentityRole, RoleViewModel>();
        }
    }
}
