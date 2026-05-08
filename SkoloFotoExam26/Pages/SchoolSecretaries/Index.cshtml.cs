using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<SchoolSecretary, int> _schoolSecRepoAsync;
        public List<SchoolSecretary> Secretaries { get; set; }
        public IndexModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo)
        {
            _schoolSecRepoAsync = schoolSecRepo;
        }
        public async Task OnGet()
        {
            Secretaries = await _schoolSecRepoAsync.GetAllAsync();
        }
    }
}
