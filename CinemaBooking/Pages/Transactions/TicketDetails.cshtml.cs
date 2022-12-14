using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CinemaBooking.Pages.Transactions
{
    public class T_DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public T_DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Tickets Tickets { get; set; }
        public Customer Customer { get; set; }
        public BuysTicket BuysTicket { get; set; } 
        public Transaction Transaction { get; set; }
        [AllowNull]
        public Cinema Cinema { get; set; }
        [AllowNull]
        public TheaterRooms Room { get; set; }
        public Seats Seats { get; set; }
        public void OnGet(int trans_id, int c_id)
        {
            //Bring up the record in buys ticket with the same customer id and transaction id, was originally going to be
            //a list but do to timing issues the record should contain just the 1 correct record or null
            BuysTicket = _db.BuysTicket.Where(b => b.CustID == c_id && b.TransactionID == trans_id).FirstOrDefault();
            //find the ticket 
            Tickets = _db.Tickets.Where(t => t.TicketNum == BuysTicket.TicketNum).FirstOrDefault();
            if (Tickets != null)
            {
                Cinema = _db.Cinema.Find(Tickets.CinemaID);
                Room = _db.TheaterRoom.Find(Tickets.TheaterID);
            }
        }
        public async Task<IActionResult> OnPost(int t_id, int trans_id, int c_id)
        {
            //Bring up the record in buys ticket with the same customer id and transaction id, was originally going to be
            //a list but do to timing issues the record should contain just the 1 correct record or null
            BuysTicket = _db.BuysTicket.Where(b => b.CustID == c_id && b.TransactionID == trans_id).FirstOrDefault();
            //find the ticket 
            Tickets = _db.Tickets.Where(t => t.TicketNum == BuysTicket.TicketNum).FirstOrDefault();
            Transaction = _db.Transaction.Where(u => u.CustID == c_id && u.TransactionID == BuysTicket.TransactionID).FirstOrDefault();
            if (Transaction != null && BuysTicket != null && Tickets != null)
            {
                Seats = _db.Seats.Where(s => s.TheaterID == Tickets.TheaterID && s.TicketNum == Tickets.TicketNum).FirstOrDefault();
                Transaction.total = Transaction.total - Tickets.Price;
                //Reset Seats table to the default
                Seats.Availabe = 1;
                Seats.TicketNum = Convert.ToInt32(null);
                _db.Seats.Update(Seats);
                //Delete Tables starting at the lowest in the hierarchy
                _db.Tickets.Remove(Tickets);
                _db.BuysTicket.Remove(BuysTicket);
                //Determine it the transaction still has other tickets by checking the total saved
                if (Transaction.total <= 0)
                {
                    _db.Transaction.Remove(Transaction);
                }
                else
                {
                    _db.Update(Transaction);
                }
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("/Transactions/List");
        }
    }
}
