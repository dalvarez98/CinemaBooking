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
        public BuysTicket Buys { get; set; }

        public Movie Movie { get; set; }    
        public Customer Customer { get; set; }
        public Cinema Cinema { get; set; }
        public Seats Seats { get; set; }
        public Tickets Tickets { get; set; }
        public Transaction Transaction { get; set; }
        public class CinemaTime
        {
            public CinemaTime()
            {
                Cinema = "null";
                Date = "null";
                Movie = "null";
            }
            public CinemaTime(string cinema, string date, string movie, int theater, int seat)
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
                Theater = c.Theater;
                Seat = c.Seat;
            }

            public string Cinema { get; set; }
            public string Date { get; set; }
            public string Movie { get; set; }
            public int Theater { get; set; }
            public int Seat { get; set; }
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
        public async Task<IActionResult> OnPost(CinemaTime c, string number, string type)
        {
            cinemaTime = new CinemaTime();
            cinemaTime.Copy(c);
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(cinemaTime.Movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));


                Tickets tickets = new Tickets();
                tickets.Price = Convert.ToDecimal(20);
                tickets.ShowDate = Convert.ToDateTime(cinemaTime.Date).Date;
                tickets.ShowTime = Convert.ToDateTime(cinemaTime.Date);
                Cinema = _db.Cinema.Where(u => u.Name.Equals(cinemaTime.Cinema)).FirstOrDefault();
                tickets.CinemaID = Cinema.CinemaID;
                tickets.TheaterID = cinemaTime.Theater;
                tickets.SeatNum = cinemaTime.Seat;
                await _db.Tickets.AddAsync(tickets);
                await _db.SaveChangesAsync();

            Buys = new BuysTicket();
            tickets = _db.Tickets.Where(u => u.CinemaID == Cinema.CinemaID && u.TheaterID == cinemaTime.Theater && u.SeatNum == cinemaTime.Seat).FirstOrDefault();
            Buys.TicketNum = tickets.TicketNum;//Placeholder
            Buys.CustID = Customer.CustID;
            Seats = _db.Seats.Where(u => u.TheaterID == cinemaTime.Theater && c.Seat == cinemaTime.Seat).FirstOrDefault();
            Seats.TicketNum = tickets.TicketNum;
            _db.Seats.Update(Seats);
            await _db.SaveChangesAsync();
            Transaction = new Transaction();
            Transaction.CustID = Customer.CustID;
            Transaction.total = tickets.Price;
            Transaction.Date = Convert.ToDateTime(cinemaTime.Date);
            Transaction.CreditCNum = number;
            Transaction.CardType = type;
            await _db.Transaction.AddAsync(Transaction);
            await _db.SaveChangesAsync();

            Transaction = _db.Transaction.Where(u => u.CustID == Customer.CustID && u.Date.Equals(Convert.ToDateTime(cinemaTime.Date)) && u.CreditCNum.Equals(number) && u.CardType.Equals(type)).FirstOrDefault(); 
            Buys.TransactionID = Transaction.TransactionID;
            await _db.BuysTicket.AddAsync(Buys);
            await _db.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
