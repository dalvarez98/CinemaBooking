using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CinemaBooking.Pages.Transactions
{
    public class T_DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public T_DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Tickets Tickets { get; set; }
        public Cinema Cinema { get; set; }
        public Room Room { get; set; }
        public void OnGet(int id)
        {
           
            Tickets = _db.Tickets.Where(t => t.TicketNum == id).FirstOrDefault();
            Cinema = _db.Cinema.Find(Tickets.CinemaID);
            Room = _db.TheaterRoom.Find(Tickets.TheaterID);
        }
    }
}
