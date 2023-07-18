using Moq;
using MSTestProject.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTestProject.NUnitTests.Mocking
{
    [TestFixture]
    public class OrderServiceTests
    {
        private OrderService _service;
        private Mock<IStorage> _storage;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IStorage>();
            _service = new OrderService(_storage.Object);
        }

        [Test]
        public void PlaceOrder_WhenCalled_ShouldStoreOrder()
        {
            var order = new Order();
            
            _service.PlaceOrder(order);

            _storage.Verify(s => s.Store(order));
        }
    }
}
