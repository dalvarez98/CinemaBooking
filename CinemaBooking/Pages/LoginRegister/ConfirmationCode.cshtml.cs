using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Pages.LoginRegister
{
    //Checks that the User is who they say they are by checking the sent confirmation code
    public class ConfirmationCodeModel : PageModel
    {
        [BindProperty]
        public ConfirmationCodeInfo ConfirmationCodeInfo { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            //For testing purposes code if hard coded
            string confCode = "98";
            //If Customer sends them to Customer Page
            if (ConfirmationCodeInfo.Code == confCode)
            {
                return RedirectToPage("/LoginRegister/ResetPassword");
            }

            ModelState.AddModelError("", "Incorrect Confirmation Code");
            return Page();
        }
    }

    public class ConfirmationCodeInfo
    {
        [Required]
        public string Code { get; set; }
    }
}
