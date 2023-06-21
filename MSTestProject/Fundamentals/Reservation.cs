namespace MSTestProject.Fundamentals
{
    public class Reservation
    {
        public User MadeBy { get; set; }
        public Reservation(User madeBy = null)
        {
            MadeBy = madeBy;
        }

        public bool CanBeCancelledBy(User user)
        {
            return user.IsAdmin || MadeBy == user;
        }
    }

    public class User
    {
        public bool IsAdmin { get; set; }
    }
}
