using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CinemaBooking.Pages.LoginRegister
{
    [BindProperties]
    public class ResetPasswordModel : PageModel
    {
        public Customer cust { get; set; }
        public void OnGet()
        {
            this.cust = new Customer();
            cust.CustID = Convert.ToInt32(TempData["Password"]);
            try
            {
                using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    String myCommand = "SELECT FirstN, LastN, Email, Password FROM Customer WHERE CustID = " + cust.CustID;
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
                    cust.Password = reader.GetChar(ordPassword);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error");
            }
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    connection.Open();
                    String myCommand = "UPDATE Customer SET FirstN = @fn, LastN = @ln, Email = @ea, Password = @pass WHERE CustID = " + cust.CustID;
                    SqlCommand command = new SqlCommand(myCommand, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    command.Parameters.Add("@fn", SqlDbType.NVarChar, 50).Value = cust.FirstN;
                    command.Parameters.Add("@ln", SqlDbType.NVarChar, 50).Value = cust.LastN;
                    command.Parameters.Add("@ea", SqlDbType.NVarChar, 50).Value = cust.Email;
                    command.Parameters.Add("@pass", SqlDbType.Char, 8).Value = cust.Password;

                    adapter.UpdateCommand = command;
                    adapter.UpdateCommand.ExecuteNonQuery();

                    command.Dispose();
                    connection.Close();
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error");
            }

            TempData["success"] = "Password Successfully Reset";
            return Page();
        }
    }
}
