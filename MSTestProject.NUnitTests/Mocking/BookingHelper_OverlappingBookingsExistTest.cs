using Moq;
using MSTestProject.Mocking;

namespace MSTestProject.NUnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTest
    {
        private Booking _existingBooking;
        private Mock<IBookingStorage> _bookingStorage;
        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2023, 7, 5),
                DepartureDate = DepartureOn(2023, 7, 15),
                Status = "Active",
                Reference = "http://example.com/booking/2"
            };

            _bookingStorage = new Mock<IBookingStorage>();
            _bookingStorage.Setup(bs => bs.GetActiveBooking(1)).Returns(new List<Booking> { _existingBooking }.AsQueryable());
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking 
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate, 1),
                DepartureDate = Before(_existingBooking.DepartureDate, 1),
                Status = "Cancelled",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void NewBookingStartsFinishesBeforeExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
                DepartureDate = Before(_existingBooking.ArrivalDate, 1),
                Status = "Active",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void NewBookingStartsBeforeButFinishesAfterExistingBooking_ReturnsExistingBookingRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 1),
                DepartureDate = After(_existingBooking.DepartureDate, 1),
                Status = "Active",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsBeforButFinisheInTheMiddleOfExistingBooking_ReturnsExistingBookingRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 1),
                DepartureDate = After(_existingBooking.ArrivalDate, 1),
                Status = "Active",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsAndFinisheInTheMiddleOfExistingBooking_ReturnsExistingBookingRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate, 1),
                DepartureDate = Before(_existingBooking.DepartureDate, 2),
                Status = "Active",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsInTheMiddleAndFinisheAfterOfExistingBooking_ReturnsExistingBookingRef()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate, 1),
                DepartureDate = After(_existingBooking.DepartureDate, 1),
                Status = "Active",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsAndFinisheAfterOfExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate, 1),
                DepartureDate = After(_existingBooking.DepartureDate, 2),
                Status = "Active",
                Reference = "http://example.com/booking/1"
            }, _bookingStorage.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartureOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
