using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {        
        private readonly ICartsManager cartsManager;
        private readonly IProductsManager productsManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CartController(ICartsManager cartsManager, IProductsManager productsManager, UserManager<User> userManager, IMapper mapper)
        {
            this.productsManager = productsManager;
            this.cartsManager = cartsManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var cart = await cartsManager.TryGetByUserIdAsync(user.UserName);            
            return View(mapper.Map<CartViewModel>(cart));
        }

        public async Task<IActionResult> Add(Guid productId)
        {
            var product = await productsManager.TryGetByIdAsync(productId);
            var user = await userManager.GetUserAsync(HttpContext.User);
            await cartsManager.AddAsync(product, user.UserName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(Guid productId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await cartsManager.RemoveAsync(productId, user.UserName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await cartsManager.ClearAsync(user.UserName);
            return RedirectToAction("Index");
        }
    }
}
