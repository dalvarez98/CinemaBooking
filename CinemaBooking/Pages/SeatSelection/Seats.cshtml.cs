using CinemaBooking.Data;
using CinemaBooking.Model;
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
        public Room Room { get; set; }
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
        public void OnGet(CinemaTime cinemaTime)
        {
            CinemaTime c = new CinemaTime();
            c.Copy(cinemaTime);
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(c.Movie)).FirstOrDefault();
            Cinema = _db.Cinema.Where(u => u.Name.Equals(c.Cinema)).FirstOrDefault();
            Room T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
            Seat = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom);
        }
        public async Task<IActionResult> OnPostAsync(CinemaTime cinemaTime, int id)
        {
            CinemaTime c = new CinemaTime();
            c.Copy(cinemaTime);

            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(c.Movie)).FirstOrDefault();
            Cinema = _db.Cinema.Where(u => u.Name.Equals(c.Cinema)).FirstOrDefault();
            Room T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
            Seat = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom && (u.SeatNum == id)).ToList();
            foreach(var s in Seat) 
            { 
                if (s == null) return Page();
                s.Availabe = 0;
                //Save SeatID for ticket
                cinemaTime.Seat = s.SeatNum;
                cinemaTime.Theater = s.TheaterID;
                _db.Seats.Update(s);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Buys/ticket", cinemaTime);
            }
            return Page();
        }
    }
}
