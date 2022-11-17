using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CinemaBooking.Pages
{
    public class Customer_SignIn_Model : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Customer Customer { get; set; }

        public Customer_SignIn_Model(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(String user, String pass)
        {
            Customer = _db.Customer.Where(u => u.Username.Equals(user) && u.Password.Equals(pass)).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
            return RedirectToPage("Edit", Customer.CustID);
        }
    }
}