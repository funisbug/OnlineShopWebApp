using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Tests.Controllers
{
    public class CartControllerTests
    {
        private readonly Mock<ICartsManager> cartsManagerMock;
        private readonly Mock<IProductsManager> productsManagerMock;
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly CartController controller;

        public CartControllerTests()
        {
            cartsManagerMock = new Mock<ICartsManager>();
            productsManagerMock = new Mock<IProductsManager>();
            userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            mapperMock = new Mock<IMapper>();
            controller = new CartController(cartsManagerMock.Object, productsManagerMock.Object, userManagerMock.Object, mapperMock.Object);

            var user = new User { UserName = "testuser" };
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) });
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };

            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            userManagerMock.Setup(manager => manager.GetUserAsync(principal)).ReturnsAsync(user);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithCartViewModel()
        {
            // Arrange
            var cart = new Cart();
            var cartViewModel = new CartViewModel();

            cartsManagerMock.Setup(manager => manager.TryGetByUserIdAsync("testuser")).ReturnsAsync(cart);
            mapperMock.Setup(mapper => mapper.Map<CartViewModel>(cart)).Returns(cartViewModel);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(cartViewModel, viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Add_RedirectsToIndexAction()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product();

            productsManagerMock.Setup(manager => manager.TryGetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await controller.Add(productId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Remove_RedirectsToIndexAction()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            var result = await controller.Remove(productId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Clear_RedirectsToIndexAction()
        {
            // Act
            var result = await controller.Clear();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}