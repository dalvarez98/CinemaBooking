using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

/*
 * Delete for Crewmember
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [BindProperties]
    public class DeleteCrewmemberModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Crewmember crewmember { get; set; }

        public DeleteCrewmemberModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Retrieves the data from the database for specified crewmember and then populates the form
        public void OnGet(int id)
        {
            crewmember = _db.Crewmember.Find(id);
        }

        public async Task<IActionResult> OnPost(Crewmember crewmember)
        {
            var crewmemberFromDb = _db.Crewmember.Find(crewmember.EmpID);

            try
            {
                if (crewmemberFromDb != null)
                {
                    using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                    {
                        connection.Open();
                        String myCommand = "DELETE FROM Crewmember WHERE EmpID = " + crewmemberFromDb.EmpID;
                        SqlCommand command = new SqlCommand(myCommand, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        adapter.DeleteCommand = command;
                        adapter.DeleteCommand.ExecuteNonQuery();

                        await _db.SaveChangesAsync();

                        command.Dispose();
                        connection.Close();

                        TempData["success"] = "Crewmember Deleted Successfully";
                    }
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                TempData["error"] = "Sorry, we are unable to proces your request at this time. Please try again later.";
            }

            return RedirectToPage("CrewmemberList");
        }
    }
}
