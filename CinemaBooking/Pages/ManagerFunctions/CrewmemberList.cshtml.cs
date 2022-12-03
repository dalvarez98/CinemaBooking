using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

/*
 * Index for Crewmembers
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [Authorize(Policy = "ManagerCredentialsRequired")]
    public class CrewmemberListModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Crewmember> ListOfCrewmember { get; set; }

        //Database Context
        public CrewmemberListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            ListOfCrewmember = _db.Crewmember; //Automatically opens db connection and executes SQL queries
        }

        //Generate report button functionality. Allows the user to download a .csv file containing all crewmember records
        public ActionResult OnPostGenerateReport()
        {
            var builder = new StringBuilder();
            builder.AppendLine("EmpID,FirstN,LastN,DOB,SSN,Address,City,State,ZipCode,Email,PhoneNum,Salary,Username,CinemaID");
            foreach(var crewmember in _db.Crewmember)
            {
                builder.AppendLine($"{crewmember.EmpID},{crewmember.FirstN},{crewmember.LastN},{crewmember.DOB},{crewmember.SSN},{crewmember.Address},{crewmember.City},{crewmember.State},{crewmember.ZipCode},{crewmember.Email},{crewmember.PhoneNum},{crewmember.Salary},{crewmember.Username},{crewmember.CinemaID}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Crewmember_Report.csv");
        }
    }
}
