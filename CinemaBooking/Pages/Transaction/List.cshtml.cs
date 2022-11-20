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

        public Transaction Transaction { get; set; }

        public ListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Customer = _db.Customer.Find(id);
            Transaction = _db.Transaction.Where(u => u.CustID = Customer.CustID);

        }
    }
}
