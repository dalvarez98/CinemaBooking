using CinemaBooking.Data;
using CinemaBooking.Model;
using CinemaBooking.Pages.ManagerFunctions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NuGet.Protocol;

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
        public TicketModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(string movie, string date, string cinema)
        {
            Movie = _db.Movie.Where(u=>u.MovieTitle.Equals(movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
        }

        public int FindSeat(Room room)
        {
            
            IEnumerable<Seats> S = _db.Seats.Where(u => u.TheaterID == room.TheaterRoom).ToList();
            foreach(var obj in S)
            {
                if (obj.Availabe != 0)
                {
                    return obj.SeatNum;
                }
                
            }
            return 0;
        }
        public async Task<IActionResult> OnPost(string movie, string date, string cinema, string number, string type)
        {
            Movie = _db.Movie.Where(u => u.MovieTitle.Equals(movie)).FirstOrDefault();
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
            Room T_room = _db.TheaterRoom.Where(u => u.MovieID == Movie.MovieID && u.CinemaID == Cinema.CinemaID).FirstOrDefault();
            int id = FindSeat(T_room);
            if (id == 0) return RedirectToPage("../Index");
            Seats = _db.Seats.Where(u => u.TheaterID == T_room.TheaterRoom && (u.SeatNum == id)).FirstOrDefault();

            Tickets tickets = new Tickets();
                tickets.Price = Convert.ToDecimal(20);
                tickets.ShowDate = Convert.ToDateTime(date).Date;
                tickets.ShowTime = Convert.ToDateTime(date);
                Cinema = _db.Cinema.Where(u => u.Name.Equals(cinema)).FirstOrDefault();
                tickets.CinemaID = Cinema.CinemaID;
                tickets.TheaterID = Seats.TheaterID;
                tickets.SeatNum = Seats.SeatNum;
                await _db.Tickets.AddAsync(tickets);
                await _db.SaveChangesAsync();

            Buys = new BuysTicket();
            tickets = _db.Tickets.Where(u => u.CinemaID == Cinema.CinemaID && u.TheaterID == Seats.TheaterID && u.SeatNum == Seats.SeatNum).FirstOrDefault();
            Buys.TicketNum = tickets.TicketNum;//Placeholder
            Buys.CustID = Customer.CustID;
            Seats.TicketNum = tickets.TicketNum;
            _db.Seats.Update(Seats);
            await _db.SaveChangesAsync();
            Transaction = new Transaction();
            Transaction.CustID = Customer.CustID;
            Transaction.total = tickets.Price;
            Transaction.Date = Convert.ToDateTime(date);
            Transaction.CreditCNum = number;
            Transaction.CardType = type;
            await _db.Transaction.AddAsync(Transaction);
            await _db.SaveChangesAsync();

            Transaction = _db.Transaction.Where(u => u.CustID == Customer.CustID && u.Date.Equals(Convert.ToDateTime(date)) && u.CreditCNum.Equals(number) && u.CardType.Equals(type)).OrderByDescending(u => u.TransactionID).FirstOrDefault(); 
            Buys.TransactionID = Transaction.TransactionID;
            await _db.BuysTicket.AddAsync(Buys);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Transactions/List");
        }
    }
}
