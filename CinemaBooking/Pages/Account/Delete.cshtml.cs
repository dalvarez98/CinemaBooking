using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBooking.Pages
{
    public class Customer_Delete_Model : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Customer Customer { get; set; }

        public Customer_Delete_Model(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Customer = _db.Customer.Find(id);
        }
        public async Task<IActionResult> OnPost(int id)
        {
      
            var customerFromDb = _db.Customer.Find(id);
            if (customerFromDb != null)
            {
                _db.Customer.Remove(customerFromDb);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("../Index");
        }
    }
}
