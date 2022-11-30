using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class TheaterRooms
    {
        [Key]
        public int TheaterRoom { get; set; }
        public int Capacity { get; set; }
        public int MovieID { get; set; } //Foreign Key
        public int CinemaID { get; set; } //Foreign Key
    }
}
