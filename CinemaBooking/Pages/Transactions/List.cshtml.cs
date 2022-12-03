using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;

namespace CinemaBooking.Pages.Transactions
{
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        [AllowNull]
        public Customer Customer { get; set; }
        [AllowNull]
        public IEnumerable<Transaction> Payment { get; set; }
        public IEnumerable<BuysTicket> BuyTickets { get; set; }

        public ListModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Transaction> GetTransactions(Customer customer)
        {
            IEnumerable<Transaction> transactions = _db.Transaction.Where(u => u.CustID == customer.CustID).OrderByDescending(u => u.Date).ToList();
            return transactions;
        }

        public void OnGet()
        {
            Customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("Userid").Value));
            Payment = GetTransactions(Customer);
        }   
    }
}
