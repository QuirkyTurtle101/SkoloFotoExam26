using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Profile
{
    public class ProfileRedirectModel : PageModel
    {
        public IActionResult OnGetRedirect()
        {
            UserType? userType = (UserType)HttpContext.Session.GetInt32("UserType");
            switch (userType)
            {
                case UserType.Administrator:
                    return RedirectToPage("/Profile/ProfileAdmin");
                case UserType.Parent:
                    //redirect here
                case UserType.Photographer:
                    //redirect here
                case UserType.SchoolSecretary:
                    //redirect here
                case UserType.Teacher:
                    //redirect here
                default:
                    return RedirectToPage("/Index");
            }
        }
    }
}
