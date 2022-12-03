using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Principal;

/*
 * This will log the user out of the website deleting their cookie and requiring them to log in again
 * if trying to access certain functionality
 */

namespace CinemaBooking.Pages.LoginRegister
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            HttpContext.User = new GenericPrincipal(new GenericIdentity(String.Empty), null);

            return RedirectToPage("/Index");
        }
    }
}
