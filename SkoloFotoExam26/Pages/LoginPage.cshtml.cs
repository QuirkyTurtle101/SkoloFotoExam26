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
        Dictionary<UserType, IRepoAsync> repositories;

        public LoginPageModel(IRepoAsync<Administrator, int> adminRepo, IRepoAsync<Parent, int> parentRepo, IRepoAsync<Photographer, int> photographerRepo, IRepoAsync<SchoolSecretary, int> schoolSecretaryRepo, IRepoAsync<Teacher, int> teacherRepo)
        {
            repositories = new Dictionary<UserType, IRepoAsync>();
            repositories.Add(UserType.Administrator, adminRepo);
            repositories.Add(UserType.Parent, parentRepo);
            repositories.Add(UserType.Photographer, photographerRepo);
            repositories.Add(UserType.SchoolSecretary, schoolSecretaryRepo);
            repositories.Add(UserType.Teacher, teacherRepo);
        }

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
            Task<object> handled = handler.HandleLoginAttempt();
            var result = await handled;
            //TODO finish
            if (result is UserType)
            {
                IRepoAsync repoToUse = repositories[(UserType)result];
                User user = await handler.GetUser((ILoginableRepo)repoToUse);
                //DO SOMETHING WITH THE USER HERE!!! maybe have a singleton that carries the information about the user from page to page? ideas, ideas...
                //for now though it compiles so what the hell right?
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
