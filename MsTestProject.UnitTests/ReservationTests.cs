using MSTestProject.Fundamentals;

namespace MsTestProject.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        // ConventionName of tests
        // NameOfMethod_Scenario_ExpectedBehavior

        // AAA convention inside tests.
        [TestMethod]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrage
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_SameUserCancellingReservation_ReturnsTrue()
        {
            var reservation = new Reservation(new User());

            var result = reservation.CanBeCancelledBy(reservation.MadeBy);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_AnotherUserCancellingReservation_ReturnsFalse()
        {
            var reservation = new Reservation(new User());
            var fakeUser = new User();

            var result = reservation.CanBeCancelledBy(fakeUser);

            Assert.IsFalse(result);
        }
    }
}