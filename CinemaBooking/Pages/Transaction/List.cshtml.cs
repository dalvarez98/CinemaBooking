using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBooking.Pages.Transaction
{
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Customer Customer { get; set; }
        public IEnumerable<Transactions> Payment { get; set; }
        public Transactions transaction { get; set; }
        public ListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Customer = _db.Customer.Find(id);
            Payment = _db.Transactions.Where(u => u.CustID == Customer.CustID).OrderByDescending(transaction => transaction.Date);

        }
    }    
}
