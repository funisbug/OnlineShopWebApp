using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductsManager> productsManagerMock;
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly Mock<IReviewApiClient> reviewApiClientMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly ProductController controller;

        public ProductControllerTests()
        {
            productsManagerMock = new Mock<IProductsManager>();
            userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            reviewApiClientMock = new Mock<IReviewApiClient>();
            mapperMock = new Mock<IMapper>();
            controller = new ProductController(productsManagerMock.Object, userManagerMock.Object, reviewApiClientMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithProductViewModel()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var reviews = new List<ReviewViewModel> { new ReviewViewModel() };
            var productViewModel = new ProductViewModel { Id = productId };

            productsManagerMock.Setup(manager => manager.TryGetByIdAsync(productId)).ReturnsAsync(product);
            reviewApiClientMock.Setup(client => client.GetByProductIdAsync(productId)).ReturnsAsync(reviews);
            mapperMock.Setup(mapper => mapper.Map<ProductViewModel>(product)).Returns(productViewModel);

            // Act
            var result = await controller.Index(productId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(productViewModel, viewResult.ViewData.Model);
        }

        [Fact]
        public async Task AddReview_RedirectsToIndexAction()
        {
            // Arrange
            var user = new User { Id = "testuser" };
            var productViewModel = new ProductViewModel { NewReview = new NewReviewViewModel { ProductId = Guid.NewGuid() } };

            userManagerMock.Setup(manager => manager.GetUserAsync(controller.User)).ReturnsAsync(user);
            reviewApiClientMock.Setup(client => client.AddAsync(productViewModel.NewReview)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.AddReview(productViewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(productViewModel.NewReview.ProductId, redirectToActionResult.RouteValues["id"]);
        }
    }
}