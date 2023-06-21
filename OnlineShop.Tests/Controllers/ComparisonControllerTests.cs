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
    public class ComparisonControllerTests
    {
        private readonly Mock<IComparisonManager> comparisonManagerMock;
        private readonly Mock<IProductsManager> productsManagerMock;
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly ComparisonController controller;

        public ComparisonControllerTests()
        {
            comparisonManagerMock = new Mock<IComparisonManager>();
            productsManagerMock = new Mock<IProductsManager>();
            userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            mapperMock = new Mock<IMapper>();
            controller = new ComparisonController(comparisonManagerMock.Object, productsManagerMock.Object, userManagerMock.Object, mapperMock.Object);

            var user = new User { UserName = "testuser" };
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) });
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };

            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

            userManagerMock.Setup(manager => manager.GetUserAsync(principal)).ReturnsAsync(user);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithComparisonViewModel()
        {
            // Arrange            
            var comparison = new Comparison();
            var comparisonViewModel = new ComparisonViewModel();

            comparisonManagerMock.Setup(manager => manager.TryGetByUserIdAsync("testuser")).ReturnsAsync(comparison);
            mapperMock.Setup(mapper => mapper.Map<ComparisonViewModel>(comparison)).Returns(comparisonViewModel);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(comparisonViewModel, viewResult.ViewData.Model);
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
    }
}