using CinemaBooking.Data;
using CinemaBooking.Model;
using CinemaBooking.Pages.LoginRegister;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CinemaBooking.Pages
{
    public class BuyModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Movie Movies { get; set; }    
        public Customer Customer { get; set; }
        public IQueryable<BuysTicket> Buy { get; set; }
        public BuyModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {

        }
    }
}
