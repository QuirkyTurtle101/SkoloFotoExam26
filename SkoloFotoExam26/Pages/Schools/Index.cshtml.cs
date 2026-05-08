using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Schools
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<School, int> _schoolRepo;

        public List<School> Schools { get; set; }

        public IndexModel(IRepoAsync<School, int> schoolRepoAsync)
        {
            _schoolRepo = schoolRepoAsync;
        }
        public async Task OnGet()
        {
            Schools = await _schoolRepo.GetAllAsync();
        }
    }
}
