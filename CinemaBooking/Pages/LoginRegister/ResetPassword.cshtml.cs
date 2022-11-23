using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CinemaBooking.Pages.LoginRegister
{
    [BindProperties]
    public class ResetPasswordModel : PageModel
    {
        public Customer cust { get; set; }
        public void OnGet()
        {
            this.cust = new Customer();
            try
            {
                using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    String myCommand = "SELECT FirstN, LastN, Email, Password FROM Customer WHERE CustID = " + TempData["Password"];
                    SqlCommand cmd = new SqlCommand(myCommand, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    int ordFName = reader.GetOrdinal("FirstN");
                    int ordLName = reader.GetOrdinal("LastN");
                    int ordEmail = reader.GetOrdinal("Email");
                    int ordPassword = reader.GetOrdinal("Password");

                    reader.Read();

                    cust.FirstN = reader.GetString(ordFName);
                    cust.LastN = reader.GetString(ordLName);
                    cust.Email = reader.GetString(ordEmail);
                    cust.Password = reader.GetString(ordPassword);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
