using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsManager productsManager;
        private readonly IMapper mapper;

        public HomeController(IProductsManager productsManager, IMapper mapper)
        {
            this.productsManager = productsManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productsManager.GetAllAsync();            
            return View(mapper.Map<List<ProductViewModel>>(products));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            var products = await productsManager.SearchAsync(name);
            return View(mapper.Map<List<ProductViewModel>>(products));
        }
    }
}
