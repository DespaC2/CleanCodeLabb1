using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop;
using WebShop.Controllers;
using WebShop.Repositories;
using WebShop.UnitOfWork;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        var notifier = new ProductNotifier(); 

        _mockUnitOfWork.Setup(uow => uow.Products).Returns(_mockProductRepository.Object);

        _controller = new ProductController(_mockUnitOfWork.Object, notifier);
    }


    [Fact]
    public async Task GetProducts_ReturnsOkWithProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1" },
            new Product { Id = 2, Name = "Product2" }
        };
        _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetProducts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsType<List<Product>>(okResult.Value);
        Assert.Equal(products.Count, returnedProducts.Count);
        Assert.Equal(products, returnedProducts);
    }

    [Fact]
    public async Task GetProduct_ReturnsOkWithProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1" };
        _mockProductRepository.Setup(repo => repo.GetByIdAsync(product.Id)).ReturnsAsync(product);

        // Act
        var result = await _controller.GetProduct(product.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(product, returnedProduct);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockProductRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProduct(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddProduct_ReturnsCreatedAtAction_WhenProductIsAdded()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "NewProduct" };

        // Act
        var result = await _controller.AddProduct(product);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetProduct), createdResult.ActionName);
        Assert.Equal(product.Id, ((Product)createdResult.Value).Id);

        _mockProductRepository.Verify(repo => repo.AddAsync(product), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddProduct_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Name is required");
        var product = new Product();

        // Act
        var result = await _controller.AddProduct(product);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsNoContent_WhenProductIsUpdated()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "UpdatedProduct" };

        // Act
        var result = await _controller.UpdateProduct(product.Id, product);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _mockProductRepository.Verify(repo => repo.UpdateAsync(product), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product1" };

        // Act
        var result = await _controller.UpdateProduct(2, product);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent_WhenProductIsDeleted()
    {
        // Arrange
        var productId = 1;

        // Act
        var result = await _controller.DeleteProduct(productId);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _mockProductRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}
