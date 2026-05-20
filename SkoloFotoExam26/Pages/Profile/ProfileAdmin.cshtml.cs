using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Profile
{
    public class ProfileAdminModel : PageModel
    {
        private IRepoAsync<Administrator, int> _adminRepo;
        public Administrator TheAdmin;

        public ProfileAdminModel(IRepoAsync<Administrator, int> adminRepo)
        {
            _adminRepo = adminRepo;
        }
        public async Task OnGetAsync()
        {
            TheAdmin = await _adminRepo.GetAsync((int)HttpContext.Session.GetInt32("UserID"));
        }
    }
}
