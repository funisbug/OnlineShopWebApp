using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShopWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsManager productsManager;

        public ProductController(IProductsManager productsManager)
        {
            this.productsManager = productsManager;
        }

        [HttpGet("GetProducts")]
        public async Task<List<Product>> GetProducts()
        {
            var products = await productsManager.GetAllAsync();
            return products;
        }

        [HttpGet("GetProduct")]
        public async Task<Product> GetProduct(Guid id)
        {
            var product = await productsManager.TryGetByIdAsync(id);
            return product;
        }

        [HttpPost("SearchProducts")]
        public async Task<List<Product>> Search(string name)
        {
            var products = await productsManager.SearchAsync(name);
            return products;
        }
    }
}
