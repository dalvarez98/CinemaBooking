using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Create for Crewmembers
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [Authorize(Policy = "ManagerCredentialsRequired")]
    [BindProperties]
    public class CreateCrewmemberModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Crewmember crewmember { get; set; }

        public CreateCrewmemberModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Creates a new profile for the Crewmember
        public async Task<IActionResult> OnPost()
        {
            await _db.Crewmember.AddAsync(crewmember);
            await _db.SaveChangesAsync();
            TempData["success"] = "New Crewmember Created Successfully";

            return RedirectToPage("CrewmemberList");
        }
    }
}
