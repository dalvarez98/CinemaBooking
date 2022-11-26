using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class TheaterRoom
    {
        public int TheaterID { get; set; } //Primary Key
        public int  Capacity { get; set; } //Foreign Key
        public int MovieID { get; set; } //Foreign Key
        public int CinemaID { get; set; }
    }
}
