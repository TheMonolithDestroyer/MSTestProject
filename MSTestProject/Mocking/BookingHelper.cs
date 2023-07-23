namespace MSTestProject.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingStorage bookingStorage)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingStorage.GetActiveBooking(booking.Id);
            var overlappingBooking = bookings.FirstOrDefault(b => booking.ArrivalDate < b.DepartureDate && b.ArrivalDate <= booking.DepartureDate);

            if (overlappingBooking == null)
                return string.Empty;
            else
                return overlappingBooking.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }

    public interface IBookingStorage
    {
        IQueryable<Booking> GetActiveBooking(int? excludedBookingId = null);
    }

    public class BookingStorage : IBookingStorage
    {
        private readonly UnitOfWork _unitOfWork;
        public BookingStorage()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IQueryable<Booking> GetActiveBooking(int? excludedBookingId = null)
        {
            var bookings = _unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");
            if (excludedBookingId == null)
                return bookings;

            return bookings.Where(i => i.Id != excludedBookingId);
        }
    }
}
