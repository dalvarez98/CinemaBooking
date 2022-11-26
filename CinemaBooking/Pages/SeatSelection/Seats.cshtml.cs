using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public Seats Seat { get; set; }
        public TheaterRoom Room { get; set; }

        bool [,] room = new bool[5,10];
        //Initalize the array
        private bool[,] InitalizeArray(bool[,] room) {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 10; j++) {
                    Seat = _db.Seats.Where(u => u.SeatNum == (i * 10 + j)).FirstOrDefault(); //Use i * 10 to convert i to a tenth place to act as a row. then add j to find the full value 
                    //Ex. I want Seat 19. this would be i = 1 * 10 which is 10 and the seat of 9 in row 1, so add 9
                    if (Seat == null)
                    {
                        room[i, j] = true;
                    }
                    else
                    {
                        room[i, j] = false;
                    }
                }
            }
            return room;
        }
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
        public void OnGet(CinemaTime c)
        {
            CinemaTime cinemaTime = new CinemaTime();
            cinemaTime.Copy(c);
            room = InitalizeArray(room);
        }
    }
}
