using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Text;

namespace CinemaBooking.Pages
{
    public class Customer_Edit_Model : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Customer Customer { get; set; }

        public Customer_Edit_Model(ApplicationDbContext db)
        {
            _db = db;
        }

        //private int db_id = Customer.CustID;
        public void OnGet(int id)
        {
            Customer = _db.Customer.Find(id);
        }
        public async Task<IActionResult> OnPost(int id)
        {
            Customer.CustID = id;
            if (ModelState.IsValid)
            {
                _db.Customer.Update(Customer);
                await _db.SaveChangesAsync();
                return RedirectToPage("../Index", Customer);
            }
            else return Page();
        }
    }
}
