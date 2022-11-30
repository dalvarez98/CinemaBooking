using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Screening
    {
        [Key]
        public int ScreeningID { get; set; }
        public int MovieID { get; set; }
        public int TheaterRoom { get; set; } 
        public DateTime ScreeningDateStart { get; set; }
        public int CinemaID { get; set; }
    }
}
