using Microsoft.AspNetCore.Mvc;
using Moq;
using WebShop;
using WebShop.Controllers;
using WebShop.Repositories;
using WebShop.UnitOfWork;

public class ProductControllerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _controller = new ProductController(_mockUnitOfWork.Object);
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithAListOfProducts()
    {
        // Arrange
        var mockProducts = new List<Product>
        {
            new Product { Id = 1, Name = "TestProduct1" },
            new Product { Id = 2, Name = "TestProduct2" }
        };

        _mockUnitOfWork.Setup(u => u.Products.GetAll()).Returns(mockProducts);

        // Act
        var result = _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Equal(2, returnedProducts.Count());
    }

    [Fact]
    public void AddProduct_ReturnsOkResult_AndCallsUnitOfWork()
    {
        // Arrange
        var newProduct = new Product { Id = 3, Name = "TestProduct3" };

        // Act
        var result = _controller.AddProduct(newProduct);

        // Assert
        _mockUnitOfWork.Verify(u => u.Products.Add(It.IsAny<Product>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.NotifyProductAdded(It.IsAny<Product>()), Times.Once);
        Assert.IsType<OkResult>(result);
    }
}
