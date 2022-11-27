using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Model
{
    public class Seats
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SeatNum { get; set; } //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TheaterID { get; set; } //Primary Key and Foreign Key
        public string RowNum { get; set; } //Foreign Key
        public int Availabe { get; set; }
        public int TicketNum { get; set; }
    }
}
