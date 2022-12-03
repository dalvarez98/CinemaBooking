using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CinemaBooking.Pages.SeatSelection
{
    public class SeatsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public SeatsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }
        [AllowNull]
        public IEnumerable<Seats> Seat { get; set; }
        public TheaterRooms Room { get; set; }
        public class CinemaTime
        {
            public CinemaTime()
            {
                Cinema = "null";
                Date = "null";
                Movie = "null";
                Theater = 0;
                Seat = 0;
            }
            public CinemaTime(string cinema, string date, string movie, int theater = 0, int seat = 0)
            {
                Cinema = cinema;
                Date = date;
                Movie = movie;
                Theater = theater;
                Seat = seat;
            }
            public void Copy(CinemaTime c)
            {
                Cinema = c.Cinema;
                Date = c.Date;
                Movie = c.Movie;
                Theater= c.Theater;
                Seat = c.Seat;

            }

            public string Cinema { get; set; }
            public string Date { get; set; }
            public string Movie { get; set; }
            public int Theater { get; set; }
            public int Seat { get; set; }
        }
        public void OnGet(string movie, string date, string cinema, string theater)
        {  
            // Find the Movie using the title
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(movie)).FirstOrDefault();
            // Find the Cinema using the title
            Cinema = _db.Cinema.Where(u => u.Name.Equals(cinema)).FirstOrDefault();
            //Find the room using both foriegn keys
            TheaterRooms T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
            //Get the list of seats in the room
            Seat = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom);
        }
        public async Task<IActionResult> OnPostAsync(string movie, string date, string cinema, int id)
        {
            //Create a cinemaTime object that will store important data that will be passed by the RedirectToPage function
            CinemaTime cinemaTime = new CinemaTime();
            cinemaTime.Movie = movie;
            cinemaTime.Date = date;
            cinemaTime.Cinema = cinema;
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(movie)).FirstOrDefault();
            Cinema = _db.Cinema.Where(u => u.Name.Equals(cinema)).FirstOrDefault();

            TheaterRooms T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
            Seat = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom && (u.SeatNum == id)).ToList();
            //Since if the Seat is available 
            foreach(var s in Seat) 
            { 
                //if not then return to page to select again
                if (s == null) return Page();
                //if it is then save seat data to cinemaTime and update the seat in the database
                if (s.Availabe != 0)
                {
                    s.Availabe = 0;
                    //Save SeatID for ticket
                    cinemaTime.Seat = s.SeatNum;
                    cinemaTime.Theater = s.TheaterID;
                    _db.Seats.Update(s);
                    await _db.SaveChangesAsync();
                    return RedirectToPage("/Buys/ticket", cinemaTime);
                }
            }
            return Page();
        }
    }
}
