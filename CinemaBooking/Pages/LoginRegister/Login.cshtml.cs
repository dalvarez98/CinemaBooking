using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

/*
 * This Page allows users to login to the website and creates an authorization profile 
 * for that user based on who they are logging in as
 */

namespace CinemaBooking.Pages.LoginRegister
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInfo LoginInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server =.; Database = CinemaBooking; Trusted_Connection = True"))
                {
                    connection.Open();
                    //Searches for User in Customer Table
                    String myCommand = "SELECT * FROM Customer WHERE Email = @EmailAddress AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(myCommand, connection);

                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = LoginInfo.Email;
                    cmd.Parameters.Add("@Password", SqlDbType.Char, 8).Value = LoginInfo.Password;
                    int idNumber = Convert.ToInt32(cmd.ExecuteScalar());

                    if (idNumber > 0)
                    { 
                        //Builds the Users Id card
                        var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, LoginInfo.Email),
                            new Claim(ClaimTypes.Email, LoginInfo.Email),
                            new Claim("UserCustomer", "Customer"),
                            new Claim("UserId", idNumber.ToString())
                    };
                        //Creates Cookie for user session
                        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                        return RedirectToPage("/Index");
                    }
                    //Searches for User in Crewmember Table if not found in Customer Table
                    myCommand = "SELECT* FROM Crewmember WHERE Email = @EmailAddress AND Password = @Password ";
                    cmd = new SqlCommand(myCommand, connection);

                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = LoginInfo.Email;
                    cmd.Parameters.Add("@Password", SqlDbType.Char, 8).Value = LoginInfo.Password;
                    idNumber = Convert.ToInt32(cmd.ExecuteScalar());

                    if (idNumber > 0)
                    {
                        //Builds the Users Id card
                        var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, LoginInfo.Email),
                            new Claim(ClaimTypes.Email, LoginInfo.Email),
                            new Claim("UserCrewmember", "Crewmember"),
                            new Claim("UserId", idNumber.ToString())
                        };
                        //Creates Cookie for user session
                        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                        return RedirectToPage("/Index");
                    }
                    //Searches for User in Manager Table if not found in Crewmember Table
                    myCommand = "SELECT EmpID, CinemaID FROM Manager WHERE Email = @EmailAddress AND Password = @Password ";
                    cmd = new SqlCommand(myCommand, connection);

                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = LoginInfo.Email;
                    cmd.Parameters.Add("@Password", SqlDbType.Char, 8).Value = LoginInfo.Password;
                    SqlDataReader reader = cmd.ExecuteReader();

                    int ordEmpID = Convert.ToInt32(reader.GetOrdinal("EmpID"));
                    int ordCinemaID = reader.GetOrdinal("CinemaID");

                    reader.Read();
                    int temp = reader.GetInt32(ordEmpID);
                    if (temp > 0)
                    {
                        //Builds the Users Id card
                        var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, LoginInfo.Email),
                        new Claim(ClaimTypes.Email, LoginInfo.Email),
                        new Claim("manager", "Manager"),
                        new Claim("UserId", reader.GetInt32(ordEmpID).ToString()),
                        new Claim("CinemaId", reader.GetInt32(ordCinemaID).ToString())
                        };
                        //Creates Cookie for user session
                        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                        return RedirectToPage("/Index");
                    }
                }
            }
            catch (SqlException)
            {
                TempData["error"] = "Sorry, we are experiencing connection issues. Please try again later.";
                return Page();
            }

            ModelState.AddModelError("", "Incorrect Email Address or Password");
            return Page();
        }
    }

    public class LoginInfo
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Minimum 8 characters required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
