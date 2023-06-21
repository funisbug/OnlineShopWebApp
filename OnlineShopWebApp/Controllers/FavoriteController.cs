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
    public class FavoriteController : Controller
    {
        private readonly IFavoritesManager favoritesManager;
        private readonly IProductsManager productsManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public FavoriteController(IFavoritesManager favoritesManager, IProductsManager productsManager, UserManager<User> userManager, IMapper mapper)
        {
            this.favoritesManager = favoritesManager;
            this.productsManager = productsManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var favoritesList = await favoritesManager.TryGetByUserIdAsync(user.UserName);
            return View(mapper.Map<FavoriteViewModel>(favoritesList));
        }

        public async Task<IActionResult> Add(Guid productId)
        {
            var product = await productsManager.TryGetByIdAsync(productId);
            var user = await userManager.GetUserAsync(HttpContext.User);
            await favoritesManager.AddAsync(product, user.UserName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(Guid productId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await favoritesManager.RemoveAsync(productId, user.UserName);
            return RedirectToAction("Index");
        }
    }
}