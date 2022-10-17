namespace CinemaBooking.Model
{
    public class Tickets
    {
        public int TicketNum { get; set; }

        public DateTime ShowTime { get; set; }

        public DateOnly ShowDate { get; set; }

        public float Price { get; set; }

        public int CinemaID { get; set; } //Foreign Key

        public int TheaterID { get; set; } //Foreign Key

        public int SeatNum  { get; set; } //Foreign Key
    }
}
