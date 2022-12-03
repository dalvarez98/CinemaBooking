using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

/*
 * Page for users to request a password change if they have forgotten theirs
 */

namespace CinemaBooking.Pages.LoginRegister
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public ForgotPasswordInfo ForgotPasswordInfo { get; set; }

        //Creates the SendGrid email object
        public ForgotPasswordModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

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
                    String myCommand = "SELECT * FROM Customer WHERE Email = @EmailAddress";
                    SqlCommand cmd = new SqlCommand(myCommand, connection);

                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = ForgotPasswordInfo.Email;
                    int idNumber = Convert.ToInt32(cmd.ExecuteScalar());

                    if (idNumber > 0)
                    {
                        string emailResetCode = "Use this code to reset password 98";
                        //EmailSender is disabled because will not work without an API Access key which will have to be provided
                        //await _emailSender.SendEmailAsync(ForgotPasswordInfo.Email, "Password Reset", emailResetCode);
                        TempData["Password"] = idNumber; //Stores the Id of User to be used for password reset
                        return RedirectToPage("/LoginRegister/ConfirmationCode");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Sorry but we are unable to process your request at the moment please try again after a couple of minutes",
                    ex.Message);
            }

            ModelState.AddModelError("", "Incorrect Email Address");
            return Page();
        }
    }

    public class ForgotPasswordInfo
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
