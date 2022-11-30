using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Tickets
    {
        [Key]
        public int TicketNum { get; set; }

        public DateTime ShowTime { get; set; }

        public DateTime ShowDate { get; set; }

        public decimal Price { get; set; }

        public int CinemaID { get; set; } //Foreign Key

        public int TheaterID { get; set; } //Foreign Key

        public int SeatNum  { get; set; } //Foreign Key
    }
}
