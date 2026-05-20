using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;
using SkoloFotoExam26.Interfaces;

namespace SkoloFotoExam26.Pages
{
    public class LoginPageModel : PageModel
    {
        [BindProperty]
        public LoginAttempt Attempt { get; set; }
        private Dictionary<UserType, IRepoAsync> repositories;
        private IRepoAsync<LoginInfo, string> _loginRepo;

        public LoginPageModel(IRepoAsync<LoginInfo, string> loginRepo, IRepoAsync<Administrator, int> adminRepo, IRepoAsync<Parent, int> parentRepo, IRepoAsync<Photographer, int> photographerRepo, IRepoAsync<SchoolSecretary, int> schoolSecretaryRepo, IRepoAsync<Teacher, int> teacherRepo)
        {
            repositories = new Dictionary<UserType, IRepoAsync>();
            repositories.Add(UserType.Administrator, adminRepo);
            repositories.Add(UserType.Parent, parentRepo);
            repositories.Add(UserType.Photographer, photographerRepo);
            repositories.Add(UserType.SchoolSecretary, schoolSecretaryRepo);
            repositories.Add(UserType.Teacher, teacherRepo);
            _loginRepo = loginRepo;
        }
        
        public void OnGet()
        {
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            LoginHandler handler = new LoginHandler(Attempt, _loginRepo);
            Task<object> handled = handler.HandleLoginAttempt();
            var result = await handled;
            //TODO finish
            if (result is UserType)
            {
                IRepoAsync repoToUse = repositories[(UserType)result];
                User user = await handler.GetUser((ILoginableRepo)repoToUse);
                HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");
                HttpContext.Session.SetInt32("UserType", (int)result);  
                HttpContext.Session.SetInt32("UserID", user.ID);
                return RedirectToPage("Index");
            }
            else if (result is string)
            {
                ViewData["ErrorMessage"] = result;
                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}
