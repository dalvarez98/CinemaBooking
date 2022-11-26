using CinemaBooking.Data;
using CinemaBooking.Model;
using CinemaBooking.Pages.ManagerFunctions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;


namespace CinemaBooking.Pages
{
    public class TicketModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Movie Movie { get; set; }    
        public Customer Customer { get; set; }
        public Cinema Cinema { get; set; }
        public Tickets Tickets { get; set; }
        public class CinemaTime
        {
            public CinemaTime()
            {
                Cinema = "null";
                Date = "null";
                Movie = "null";
            }
            public CinemaTime(string cinema, string date, string movie)
            {
                Cinema = cinema;
                Date = date;
                Movie = movie;
            }
            public void Copy(CinemaTime c)
            {
                Cinema = c.Cinema;
                Date = c.Date;
                Movie = c.Movie;
            }

            public string Cinema { get; set; }
            public string Date { get; set; }
            public string Movie { get; set; }
        }
        CinemaTime cinemaTime;
        public TicketModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(CinemaTime c)
        {
            cinemaTime = new CinemaTime();
            cinemaTime.Copy(c);
            Movie = _db.Movie.Where(u=>u.MovieTitle.Equals(cinemaTime.Movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
        }
        public async Task<IActionResult> OnPost(int amount, CinemaTime c)
        {
            cinemaTime = new CinemaTime();
            cinemaTime.Copy(c);
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(cinemaTime.Movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
            for (int i =0; i < amount; i++) 
            {
                Tickets tickets = new Tickets();
                tickets.Price = Convert.ToDecimal(20);
                tickets.ShowDate = Convert.ToDateTime(cinemaTime.Date).Date;
                tickets.ShowTime = Convert.ToDateTime(cinemaTime.Date);
                Cinema = _db.Cinema.Where(u => u.Name.Equals(cinemaTime.Cinema)).FirstOrDefault();
                tickets.CinemaID = Cinema.CinemaID;
                tickets.TheaterID = 0;
                tickets.SeatNum = 0;
                await _db.Tickets.AddAsync(tickets);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("../Index");
        }
    }
}
