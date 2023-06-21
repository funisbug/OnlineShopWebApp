using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IProductsManager> productsManagerMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly HomeController controller;

        public HomeControllerTests()
        {
            productsManagerMock = new Mock<IProductsManager>();
            mapperMock = new Mock<IMapper>();
            controller = new HomeController(productsManagerMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product>();
            var productViewModels = new List<ProductViewModel>();

            productsManagerMock.Setup(manager => manager.GetAllAsync()).ReturnsAsync(products);
            mapperMock.Setup(mapper => mapper.Map<List<ProductViewModel>>(products)).Returns(productViewModels);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(productViewModels, viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Search_ReturnsViewResult_WithFilteredProducts()
        {
            // Arrange
            var name = "test";
            var products = new List<Product>();
            var filteredProducts = new List<Product>();
            var filteredProductViewModels = new List<ProductViewModel>();

            productsManagerMock.Setup(manager => manager.SearchAsync(name)).ReturnsAsync(filteredProducts);
            mapperMock.Setup(mapper => mapper.Map<List<ProductViewModel>>(filteredProducts)).Returns(filteredProductViewModels);

            // Act
            var result = await controller.Search(name);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(filteredProductViewModels, viewResult.ViewData.Model);
        }
    }
}