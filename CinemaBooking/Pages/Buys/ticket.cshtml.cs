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
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(cinemaTime.Movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
        }
        public async Task<IActionResult> OnPost(CinemaTime c, string number, string type)
        {
            cinemaTime = new CinemaTime();
            cinemaTime.Copy(c);
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(cinemaTime.Movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
            //Cinema = _db.Cinema.Where(u => u.Name.Equals(cinema)).FirstOrDefault();
            TheaterRooms T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
            IEnumerable<Seats> S = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom).ToList();
            //Look through the available seats to find the next available SeatNum
            foreach (var obj in S)
            {
                if (obj.Availabe != 0)
                {
                    //id = obj.SeatNum;
                }
            }
            //If no seat found then redirect to home page
            //if (id == 0) return RedirectToPage("../Index");
            //Seats = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom && (u.SeatNum == id)).FirstOrDefault();
            //Create an Ticket Entity 
            Tickets tickets = new Tickets();
            tickets.Price = Convert.ToDecimal(20);
            //tickets.ShowDate = Convert.ToDateTime(date).Date;
            //tickets.ShowTime = Convert.ToDateTime(date);
            tickets.CinemaID = Cinema.CinemaID;
            tickets.TheaterID = Seats.TheaterID;
            tickets.SeatNum = Seats.SeatNum;
            //Save ticket to get the ms SQL to assign the Ticket number
            await _db.Tickets.AddAsync(tickets);
            await _db.SaveChangesAsync();
            //Create a BuysTicket Entity
            Buys = new BuysTicket();
            //Retrieve ticket to get the TicketNum Use order by to get the most resent to be added
            tickets = _db.Tickets.Where(u => u.CinemaID == Cinema.CinemaID && u.TheaterID == Seats.TheaterID && u.SeatNum == Seats.SeatNum).OrderBy(u => u.TicketNum).FirstOrDefault();
            //Assign to the BuysTicket and Seat table
            Buys.TicketNum = tickets.TicketNum;
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