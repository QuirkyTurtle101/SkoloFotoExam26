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
                    return RedirectToPage("/Profile/ProfileParent");
                case UserType.Photographer:
                    return RedirectToPage("/Profile/ProfilePhotographer");
                case UserType.SchoolSecretary:
                    return RedirectToPage("/Profile/ProfileSchoolSecretary");
                case UserType.Teacher:
                    return RedirectToPage("/Profile/ProfileTeacher");
                default:
                    return RedirectToPage("/Index");
            }
        }
    }
}
