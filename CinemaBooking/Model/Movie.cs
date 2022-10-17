using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "Please provide a Movie Title", AllowEmptyStrings = false)]
        public string MovieTitle { get; set; }

        [Required(ErrorMessage = "Please provide a Genre", AllowEmptyStrings = false)]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Please provide a Rating", AllowEmptyStrings = false)]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Please provide a Director", AllowEmptyStrings = false)]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please provide a Description", AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}
