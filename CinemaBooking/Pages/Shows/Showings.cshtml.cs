using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBooking.Pages.Shows;

public class ShowingsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    
    public IEnumerable<Movie> Showings { get; set; }

    public ShowingsModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        Showings = _db.Movie;

    }
}
