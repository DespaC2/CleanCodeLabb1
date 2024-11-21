using Moq;
using WebShop.Notifications;
using WebShop.Repositories;
using WebShop.UnitOfWork;

namespace WebShop.Tests
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void NotifyProductAdded_CallsObserverUpdate()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "TestProduct" };
            var mockObserver = new Mock<INotificationObserver>();

            var productSubject = new ProductSubject();
            productSubject.Attach(mockObserver.Object);

            var mockProductRepository = new Mock<IProductRepository>();
            var unitOfWork = new WebShop.UnitOfWork.UnitOfWork(mockProductRepository.Object, productSubject);


            // Act
            unitOfWork.NotifyProductAdded(product);

            // Assert
            mockObserver.Verify(o => o.Update(product), Times.Once);
        }
    }
}
