using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Parents
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<Parent, int> pRepo;
        public List<Parent> Parents { get; set; }
        

        public string CurrentRole { get; set; }


        public IndexModel(IRepoAsync<Parent, int> parentRepo)
        {
            pRepo = parentRepo;
        }

        public async Task OnGetAsync(string role = "parent")
        {
            CurrentRole = role.ToLower();

            if (CurrentRole == "admin")
            {
                Parents = await pRepo.GetAllAsync();
            }
            else
            {
                int loggedInParentID = 1;
                var myProfile = await pRepo.GetAsync(loggedInParentID);
                Parents = new List<Parent> { myProfile };
             }
        }
    }
}
