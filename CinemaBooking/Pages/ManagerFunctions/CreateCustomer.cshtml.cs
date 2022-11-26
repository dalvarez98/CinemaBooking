using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Create For Customers
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [BindProperties]
    public class CreateCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Customer customer { get; set; }

        public CreateCustomerModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Creates a new profile for the Customer
        public async Task<IActionResult> OnPost()
        {
            await _db.Customer.AddAsync(customer);
            await _db.SaveChangesAsync();
            TempData["success"] = "New Customer Created Successfully";

            return RedirectToPage("CustomerList");
        }
    }
}
