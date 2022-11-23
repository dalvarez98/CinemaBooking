using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBooking.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Customer Customer { get; set; }

        public CustomerModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public void OnGet(Customer cust)
        {
            Customer = _db.Customer.Find(cust.CustID);

        }
        /*public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Customer.Update(Customer);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else return Page();
        }*/
    }
}
