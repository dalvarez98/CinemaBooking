using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Model
{
    public class Room
    {
        [Key]
        public int TheaterRoom { get; set; } //Primary Key
        public int  Capacity { get; set; } //Foreign Key
        public int MovieID { get; set; } //Foreign Key
        public int CinemaID { get; set; }
    
    }
}
