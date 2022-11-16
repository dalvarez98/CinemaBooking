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
        public Customer Customer { get; set; }

        public Customer_SignIn_Model(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(String user, String pass)
        {
            Customer = _db.Customer.Where(u => u.Username == user && u.Password == pass).FirstOrDefault();
            if(Customer == null)
            {
                Console.WriteLine("Could Not Find Account");
            }
        }
        /*
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Customer.AddAsync(Customer);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else return Page();
        }*/
    }
}