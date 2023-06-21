using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsManager cartsManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CartViewComponent(ICartsManager cartsManager, UserManager<User> userManager, IMapper mapper)
        {
            this.cartsManager = cartsManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return View("Cart", 0);
            }
            var cart = await cartsManager.TryGetByUserIdAsync(user.UserName); 
            var cartViewModel = mapper.Map<CartViewModel>(cart);
            var productCount = cartViewModel?.Amount ?? 0;
            return View("Cart", productCount);
        }
    }
}
