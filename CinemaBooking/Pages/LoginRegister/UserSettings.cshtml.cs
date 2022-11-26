using CinemaBooking.Data;
using CinemaBooking.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CinemaBooking.Pages.LoginRegister
{
    [BindProperties]
    public class UserSettingsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Customer customer { get; set; }
        public Crewmember crewmember { get; set; }
        public Manager manager { get; set; }

        public UserSettingsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            if(User.HasClaim("UserCustomer", "Customer"))
            {
                customer = _db.Customer.Find(Convert.ToInt32(User.FindFirst("UserId").Value));
            }
            else if(User.HasClaim("UserCrewmember", "Crewmember"))
            {
                crewmember = _db.Crewmember.Find(Convert.ToInt32(User.FindFirst("UserId").Value));
            }
            else
            {
                manager = _db.Manager.Find(Convert.ToInt32(User.FindFirst("UserId").Value));
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (User.HasClaim("UserCustomer", "Customer"))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                    {
                        connection.Open();
                        String myCommand = "UPDATE Customer SET FirstN = @fn, LastN = @ln, Address = @add, City = @city, State = @st, ZipCode = @zip, Email = @ea, PhoneNum = @pn, Username = @u WHERE CustID = " 
                            + Convert.ToInt32(User.FindFirst("UserId").Value);
                        SqlCommand command = new SqlCommand(myCommand, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        command.Parameters.Add("@fn", SqlDbType.NVarChar, 50).Value = customer.FirstN;
                        command.Parameters.Add("@ln", SqlDbType.NVarChar, 50).Value = customer.LastN;
                        command.Parameters.Add("@add", SqlDbType.NVarChar, 100).Value = customer.Address;
                        command.Parameters.Add("@city", SqlDbType.NVarChar, 30).Value = customer.City;
                        command.Parameters.Add("@st", SqlDbType.NVarChar, 2).Value = customer.State;
                        command.Parameters.Add("@zip", SqlDbType.NVarChar, 9).Value = customer.ZipCode;
                        command.Parameters.Add("@ea", SqlDbType.NVarChar, 50).Value = customer.Email;
                        command.Parameters.Add("@pn", SqlDbType.NVarChar, 15).Value = customer.PhoneNum;
                        command.Parameters.Add("@u", SqlDbType.NVarChar, 30).Value = customer.Username;

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
            }
            else if (User.HasClaim("UserCrewmember", "Crewmember"))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                    {
                        connection.Open();
                        String myCommand = "UPDATE Crewmember SET FirstN = @fn, LastN = @ln, Address = @add, City = @city, State = @st, ZipCode = @zip, Email = @ea, PhoneNum = @pn, Username = @u WHERE EmpID = " 
                            + Convert.ToInt32(User.FindFirst("UserId").Value);
                        SqlCommand command = new SqlCommand(myCommand, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        command.Parameters.Add("@fn", SqlDbType.NVarChar, 50).Value = crewmember.FirstN;
                        command.Parameters.Add("@ln", SqlDbType.NVarChar, 50).Value = crewmember.LastN;
                        command.Parameters.Add("@add", SqlDbType.NVarChar, 100).Value = crewmember.Address;
                        command.Parameters.Add("@city", SqlDbType.NVarChar, 30).Value = crewmember.City;
                        command.Parameters.Add("@st", SqlDbType.NVarChar, 2).Value = crewmember.State;
                        command.Parameters.Add("@zip", SqlDbType.NVarChar, 9).Value = crewmember.ZipCode;
                        command.Parameters.Add("@ea", SqlDbType.NVarChar, 50).Value = crewmember.Email;
                        command.Parameters.Add("@pn", SqlDbType.NVarChar, 15).Value = crewmember.PhoneNum;
                        command.Parameters.Add("@u", SqlDbType.NVarChar, 30).Value = crewmember.Username;

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
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection("Server = .; Database = CinemaBooking; Trusted_Connection = True"))
                    {
                        connection.Open();
                        String myCommand = "UPDATE Crewmember SET FirstN = @fn, LastN = @ln, Address = @add, City = @city, State = @st, ZipCode = @zip, Email = @ea, PhoneNum = @pn, Username = @u WHERE EmpID = " 
                            + Convert.ToInt32(User.FindFirst("UserId").Value);
                        SqlCommand command = new SqlCommand(myCommand, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        command.Parameters.Add("@fn", SqlDbType.NVarChar, 50).Value = manager.FirstN;
                        command.Parameters.Add("@ln", SqlDbType.NVarChar, 50).Value = manager.LastN;
                        command.Parameters.Add("@add", SqlDbType.NVarChar, 100).Value = manager.Address;
                        command.Parameters.Add("@city", SqlDbType.NVarChar, 30).Value = manager.City;
                        command.Parameters.Add("@st", SqlDbType.NVarChar, 2).Value = manager.State;
                        command.Parameters.Add("@zip", SqlDbType.NVarChar, 9).Value = manager.ZipCode;
                        command.Parameters.Add("@ea", SqlDbType.NVarChar, 50).Value = manager.Email;
                        command.Parameters.Add("@pn", SqlDbType.NVarChar, 15).Value = manager.PhoneNum;
                        command.Parameters.Add("@u", SqlDbType.NVarChar, 30).Value = manager.Username;

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
            }

            TempData["success"] = "Settings Successfully Updated";
            return Page();
        }
    }
}
