using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsManager productsManager;
        private readonly UserManager<User> userManager;
        private readonly IReviewApiClient reviewApiClient;
        private readonly IMapper mapper;

        public ProductController(IProductsManager productsManager, UserManager<User> userManager, IReviewApiClient reviewApiClient, IMapper mapper)
        {
            this.productsManager = productsManager;
            this.userManager = userManager;
            this.reviewApiClient = reviewApiClient;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var product = await productsManager.TryGetByIdAsync(id);
            var reviews = await reviewApiClient.GetByProductIdAsync(id);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            productViewModel.Reviews = reviews;
            if (product == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ProductViewModel product)
        {
            var user = await userManager.GetUserAsync(User);
            product.NewReview.UserId = user.Id;
            await reviewApiClient.AddAsync(product.NewReview);            
            return RedirectToAction("Index", new { id = product.NewReview.ProductId });
        }
    }
}
