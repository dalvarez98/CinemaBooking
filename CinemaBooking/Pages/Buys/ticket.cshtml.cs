using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBooking.Pages
{
    public class TicketModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Movie Movie { get; set; }    
        public Customer Customer { get; set; }
        public IQueryable<Tickets> Tickets { get; set; }
        public TicketModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Movie = _db.Movie.Find(id);
            string name = User.Identity.Name;


        }
    }
}
