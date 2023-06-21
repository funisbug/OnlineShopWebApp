using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductsManager productsManager;
        private readonly ImageProvider imageProvider;
        private readonly IMapper mapper;

        public ProductsController(IProductsManager productsManager, ImageProvider imageProvider, IMapper mapper)
        {
            this.productsManager = productsManager;
            this.imageProvider = imageProvider;
            this.mapper = mapper;
        }

        
        public async Task<IActionResult> Index()
        {
            var products = await productsManager.GetAllAsync();            
            return View(mapper.Map<List<ProductViewModel>>(products));
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                product.ImagePath = imageProvider.SafeFile(product.UploadedFile, ImageFolders.Products);
                var productDb = mapper.Map<Product>(product);                
                await productsManager.AddAsync(productDb);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> EditProduct(Guid id)
        {
            var product = await productsManager.TryGetByIdAsync(id);
            return View(mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.UploadedFile != null)
                {
                    product.ImagePath = imageProvider.SafeFile(product.UploadedFile, ImageFolders.Products);
                }                
                var productDb = mapper.Map<Product>(product);
                await productsManager.UpdateAsync(productDb);
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditProduct", product.Id);
        }

        public async Task<IActionResult> RemoveProduct(Guid id)
        {
            await productsManager.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
