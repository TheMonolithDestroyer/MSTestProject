using MSTestProject.Fundamentals;

namespace MSTestProject.NUnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        // ConventionName of tests
        // NameOfMethod_Scenario_ExpectedBehavior

        // AAA convention inside tests.
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            var reservation = new Reservation();

            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingReservation_ReturnsTrue()
        {
            var reservation = new Reservation(new User());

            var result = reservation.CanBeCancelledBy(reservation.MadeBy);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancellingReservation_ReturnsFalse()
        {
            var reservation = new Reservation(new User());
            var fakeUser = new User();

            var result = reservation.CanBeCancelledBy(fakeUser);

            Assert.That(result, Is.False);
        }
    }
}