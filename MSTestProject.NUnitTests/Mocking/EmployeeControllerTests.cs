using Moq;
using MSTestProject.Mocking;

namespace MSTestProject.NUnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _controller;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeletesEmployeeFromDbAdnReturnRidectResult()
        {
            var result = _controller.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            _controller.DeleteEmployee(1);

            _storage.Verify(r => r.DeleteEmployee(1));
        }
    }
}
