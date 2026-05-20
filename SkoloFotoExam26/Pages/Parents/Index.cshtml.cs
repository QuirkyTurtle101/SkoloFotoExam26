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
        
        public IndexModel(IRepoAsync<Parent, int> parentRepo)
        {
            pRepo = parentRepo;
        }

        public async Task OnGetAsync()
        {
            Parents = await pRepo.GetAllAsync();
        }
    }
}
