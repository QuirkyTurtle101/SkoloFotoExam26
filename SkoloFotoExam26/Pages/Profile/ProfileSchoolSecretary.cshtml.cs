using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Profile
{
    public class ProfileSchoolSecretaryModel : PageModel
    {
        private IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        public SchoolSecretary TheSchoolSec { get; set; }
        public ProfileSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo)
        {
            _schoolSecRepo = schoolSecRepo;
        }
        public async Task OnGetAsync()
        {
            TheSchoolSec = await _schoolSecRepo.GetAsync((int)HttpContext.Session.GetInt32("UserID"));
        }
    }
}
