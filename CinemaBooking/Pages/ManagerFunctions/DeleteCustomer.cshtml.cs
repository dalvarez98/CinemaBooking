using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

/*
 * Delete a Customer
 */

namespace CinemaBooking.Pages.ManagerFunctions
{
    [BindProperties]
    public class DeleteCustomerModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Customer customer { get; set; }

        public DeleteCustomerModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //Retrieves the data from the database for specified customer and then populates the form
        public void OnGet(int id)
        {
            customer = _db.Customer.Find(id);
        }

        public async Task<IActionResult> OnPost(Customer customer)
        {
            var customerFromDb = _db.Customer.Find(customer.CustID);

            try
            {
                if (customerFromDb != null)
                {
                    using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                    {
                        connection.Open();
                        String myCommand = "DELETE FROM Customer WHERE CustID = " + customerFromDb.CustID;
                        SqlCommand command = new SqlCommand(myCommand, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        adapter.DeleteCommand = command;
                        adapter.DeleteCommand.ExecuteNonQuery();

                        await _db.SaveChangesAsync();

                        command.Dispose();
                        connection.Close();

                        TempData["success"] = "Customer Deleted Successfully";
                    }
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                TempData["error"] = "Sorry, we are unable to proces your request at this time. Please try again later.";
            }

            return RedirectToPage("CustomerList");
        }
    }
}
