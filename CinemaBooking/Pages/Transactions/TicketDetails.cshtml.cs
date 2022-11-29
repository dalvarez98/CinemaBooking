using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authentication;
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
        public Room Room { get; set; }
        public void OnGet(int t_id,int trans_id, int c_id)
        {
           
            Tickets = _db.Tickets.Where(t => t.TicketNum == t_id).FirstOrDefault();
            if (Tickets != null)
            {
                Cinema = _db.Cinema.Find(Tickets.CinemaID);
                Room = _db.TheaterRoom.Find(Tickets.TheaterID);
            }
        }

        public async Task<IActionResult> OnPost(int t_id, int trans_id, int c_id)
        {
            Tickets = _db.Tickets.Where(t => t.TicketNum == t_id).FirstOrDefault();
            BuysTicket = _db.BuysTicket.Where(b => b.TicketNum == t_id && b.CustID == c_id && b.TransactionID == trans_id).FirstOrDefault();
            Transaction = _db.Transaction.Where(u => u.CustID == c_id && u.TransactionID == trans_id).FirstOrDefault();
            if (Transaction != null && BuysTicket != null && Tickets != null)
            {
                Transaction.total = Transaction.total - Tickets.Price;
                if (Transaction.total <= 0)
                {
                    _db.Transaction.Remove(Transaction);
                }
                else
                {
                    _db.Update(Transaction);
                }
                _db.Tickets.Remove(Tickets);
                _db.BuysTicket.Remove(BuysTicket);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Transactions/List");
            }
            else return RedirectToPage("../Index");

        }
    }
}
