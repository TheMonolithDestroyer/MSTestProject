using MSTestProject.Fundamentals;

namespace MSTestProject.NUnitTests
{
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;
        [SetUp]
        public void SetUp()
        {
            _calculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(302)]
        public void CalculateDemeritPoints_WhenSpeedIsIncreadible_ThrowArgOutOfRangeExp(int speed)
        {
            Assert.That(() => _calculator.CalculateDemeritPoints(speed), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0)]
        [TestCase(30)]
        [TestCase(65)]
        public void CalculateDemeritPoints_WhenSpeedIsNotExceeded_ReturnsZero(int speed)
        {
            var result = _calculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_WhenSpeedIsExceeded_ReturnsDemeritPoint()
        {
            var result = _calculator.CalculateDemeritPoints(75);

            Assert.That(result, Is.EqualTo(2));
        }
    }
}
