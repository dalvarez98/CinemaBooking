using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBooking.Pages
{
    [BindProperties]
    public class CustomerModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Customer Customer { get; set; }

        public CustomerModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnPost(Customer cust)
        {
            await _db.Customer.AddAsync(cust);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
