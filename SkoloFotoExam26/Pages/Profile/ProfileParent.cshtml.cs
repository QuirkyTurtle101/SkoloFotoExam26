using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Profile
{
    public class ProfileParentModel : PageModel
    {
        private IRepoAsync<Parent, int> _parentRepo;
        public Parent TheParent { get; set; }
        public ProfileParentModel(IRepoAsync<Parent, int> parentRepo)
        {
            _parentRepo = parentRepo;
        }

        public async Task OnGetAsync()
        {
            TheParent = await _parentRepo.GetAsync((int)HttpContext.Session.GetInt32("UserID"));
        }
    }
}
