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
        public Room Room { get; set; } //Different from the room array. This represents the room as an entity

        bool [,] room = new bool[3,10];
        //Initalize the array
        private bool[,] InitalizeArray(bool[,] room, CinemaTime c) {
            for (int i = 1; i <=3; i++) {
                for (int j = 1; j <= 10; j++) {
                    Movie = _db.Movie.Where(u => u.MovieTitle.Equals(c.Movie)).FirstOrDefault();
                    Cinema = _db.Cinema.Where(u => u.Name.Equals(c.Cinema)).FirstOrDefault();
                    Room T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
                    int z = i * j;
                    Seat = _db.Seats.Where(u => u.SeatNum == z && u.TheaterID == T_room.TheaterRoom).FirstOrDefault();
                    
                    if (Seat.SeatNum == j && Seat.RowNum == Convert.ToString(i * j) && Seat.Availabe > 0)
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
            room = InitalizeArray(room, cinemaTime);
        }
        public async Task<IActionResult> OnPostAsync(CinemaTime c, int i, int j)
        {
            CinemaTime cinemaTime = new CinemaTime();
            cinemaTime.Copy(c);
            room = InitalizeArray(room, cinemaTime);
            if (room[i,j] == true)
            {
                Movie = _db.Movie.Where(u => u.MovieTitle.Equals(c.Movie)).FirstOrDefault();
                Cinema = _db.Cinema.Where(u => u.Name.Equals(c.Cinema)).FirstOrDefault();
                Room T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
                Seat = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom&& u.RowNum.Equals(Convert.ToString(i)) && (u.SeatNum == i * j)).FirstOrDefault();

                if (Seat == null) return Page();
                Seat.Availabe = 0;
                //Save SeatID for ticket
                cinemaTime.Seat = Seat.SeatNum;
                cinemaTime.Theater = Seat.TheaterID;
                _db.Seats.Update(Seat);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Buys/ticket", cinemaTime);
            }
            else
            {
                return Page();
            }

        }
    }
}
