using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Edit for Customer
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [BindProperties]
    public class EditCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Customer customer { get; set; }

        public EditCustomerModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Retrieves the data from the database for specified customer and then populates the form
        public void OnGet(int id)
        {
            customer = _db.Customer.Find(id);
        }

        //Saves changes to database
        public async Task<IActionResult> OnPost(Customer customer)
        {
            _db.Customer.Update(customer);
            await _db.SaveChangesAsync();
            TempData["success"] = "Customer Edited Successfully";

            return RedirectToPage("CustomerList");
        }
    }
}
