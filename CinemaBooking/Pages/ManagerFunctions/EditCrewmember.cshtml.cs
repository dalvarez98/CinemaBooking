using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Edit for Crewmembers
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [Authorize(Policy = "ManagerCredentialsRequired")]
    [BindProperties]
    public class EditCrewmemberModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Crewmember crewmember { get; set; }

        public EditCrewmemberModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Retrieves the data from the database for specified crewmember and then populates the form
        public void OnGet(int id)
        {
            crewmember = _db.Crewmember.Find(id);
        }

        //Saves changes to database
        public async Task<IActionResult> OnPost(Crewmember crewmember)
        {
            _db.Crewmember.Update(crewmember);
            await _db.SaveChangesAsync();
            TempData["success"] = "Crewmember Edited Successfully";

            return RedirectToPage("CrewmemberList");
        }
    }
}
