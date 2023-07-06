using MSTestProject.Fundamentals;

namespace MSTestProject.NUnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_MultiplesOf3And5_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_MultiplesOf3_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(6);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_MultiplesOf5_ReturnsBuzz()
        {
            var result = FizzBuzz.GetOutput(10);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_DoesNotMultiplesOf3Or5_ReturnsNumber()
        {
            var result = FizzBuzz.GetOutput(14);

            Assert.That(result, Is.EqualTo("14"));
        }
    }
}
