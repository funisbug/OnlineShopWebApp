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
    public class ComparisonController : Controller
    {
        private readonly IComparisonManager comparisonManager;
        private readonly IProductsManager productsManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public ComparisonController(IComparisonManager comparisonManager, IProductsManager productsManager, UserManager<User> userManager, IMapper mapper)
        {
            this.comparisonManager = comparisonManager;
            this.productsManager = productsManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var comparerList = await comparisonManager.TryGetByUserIdAsync(user.UserName);
            return View(mapper.Map<ComparisonViewModel>(comparerList));
        }

        public async Task<IActionResult> Add(Guid productId)
        {
            var product = await productsManager.TryGetByIdAsync(productId);
            var user = await userManager.GetUserAsync(HttpContext.User);
            await comparisonManager.AddAsync(product, user.UserName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(Guid productId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await comparisonManager.RemoveAsync(productId, user.UserName);
            return RedirectToAction("Index");
        }
    }
}
