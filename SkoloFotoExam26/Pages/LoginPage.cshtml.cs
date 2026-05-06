using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages
{
    public class LoginPageModel : PageModel
    {
        [BindProperty]
        public LoginAttempt Attempt { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            LoginHandler handler = new LoginHandler(Attempt);
            Task<object> handled = handler.HandleLogin();
            var result = await handled;
            //TODO finish
            if (result is User)
            {

            }
        }
    }
}
