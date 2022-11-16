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
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Customer.AddAsync(Customer);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else return Page();
        }
    }
}
