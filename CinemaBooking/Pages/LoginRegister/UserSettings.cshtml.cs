using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CinemaBooking.Pages.LoginRegister
{
    [BindProperties]
    public class UserSettingsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Customer customer { get; set; }
        public Crewmember crewmember { get; set; }

        public UserSettingsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            if(User.HasClaim("UserCustomer", "Customer"))
            {
                customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("UserId").Value));
            }
            else if(User.HasClaim("UserCrewmember", "Crewmember"))
            {

            }
            else
            {

            }
        }

        public async Task<IActionResult> OnPost()
        {
            return Page();
        }
    }
}
