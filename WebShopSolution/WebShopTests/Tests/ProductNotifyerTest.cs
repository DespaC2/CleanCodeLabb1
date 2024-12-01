using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Notifications;
using WebShop;

namespace WebShopTests.Tests
{
    public class ProductNotifierTests
    {
        [Fact]
        public void Notify_CallsUpdateOnAllObservers()
        {
            // Arrange
            var mockObserver1 = new Mock<INotificationObserver>();
            var mockObserver2 = new Mock<INotificationObserver>();
            var productNotifier = new ProductNotifier();
            var product = new Product { Id = 1, Name = "Test Product" };

            productNotifier.Attach(mockObserver1.Object);
            productNotifier.Attach(mockObserver2.Object);

            // Act
            productNotifier.Notify(product);

            // Assert
            mockObserver1.Verify(o => o.Update(product), Times.Once);
            mockObserver2.Verify(o => o.Update(product), Times.Once);
        }
    }

}
