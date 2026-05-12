using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolClasses
{
    public class IndexModel : PageModel
    {
        IRepoAsync<SchoolClass, int> _schoolClassRepo;

        public List<SchoolClass> SchoolClasses { get; set; }

        public IndexModel(IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            _schoolClassRepo = schoolClassRepo;
        }
        public async Task OnGet()
        {
            SchoolClasses = await _schoolClassRepo.GetAllAsync();
        }
    }
}
