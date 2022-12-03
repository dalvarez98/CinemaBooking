using CinemaBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

/*
 * Registration Page for a potential customer
 */

namespace CinemaBooking.Pages.LoginRegister
{
    [BindProperties]
    public class CustomerRegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Model.Customer Customer { get; set; }

        public CustomerRegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //Sends the Data of the Customer being registered to the database
                await _db.Customer.AddAsync(Customer);
                await _db.SaveChangesAsync();
                TempData["success"] = "Registration was Successful";
                return RedirectToPage("/LoginRegister/Login");
            }

            return Page();
        }
    }
}
