using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

/*
 * Index for Customers
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [Authorize(Policy = "ManagerCredentialsRequired")]
    public class CustomerListModel : PageModel
    {
        private readonly ApplicationDbContext _db;
		public IEnumerable<Customer> ListOfCustomer { get; set; }

        //Database Context
        public CustomerListModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public void OnGet()
        {
            ListOfCustomer = _db.Customer; //Automatically opens db connection and executes SQL queries
        }

        //Generate report button functionality. Allows the user to download a .csv file containing all customer records
        public ActionResult OnPostGenerateReport()
		{
            var builder = new StringBuilder();
            builder.AppendLine("CustID,FirstN,LastN,DOB,Address,City,State,ZipCode,Email,PhoneNum,Username");
            foreach(var customer in _db.Customer)
            {
                builder.AppendLine($"{customer.CustID},{customer.FirstN},{customer.LastN},{customer.DOB},{customer.Address},{customer.City},{customer.State},{customer.ZipCode},{customer.Email},{customer.PhoneNum},{customer.Username}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Customers_Report.csv");
		}
    }
}
