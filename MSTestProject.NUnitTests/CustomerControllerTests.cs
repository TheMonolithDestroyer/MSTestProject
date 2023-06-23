using MSTestProject.Fundamentals;
using static MSTestProject.Fundamentals.CustomerController;

namespace MSTestProject.NUnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _customerController;

        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetCustomer_WhenCalled_ReturnNotFound(int id) 
        {
            var result = _customerController.GetCustomer(id);

            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsGreaterThanZero_ReturnOk()
        {
            var result = _customerController.GetCustomer(3);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
