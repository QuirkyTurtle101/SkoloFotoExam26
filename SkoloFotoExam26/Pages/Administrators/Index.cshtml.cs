using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Administrators
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<Administrator, int> _adminRepo;

        public List<Administrator> Admins { get; set; }

        public IndexModel(IRepoAsync<Administrator, int> adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task OnGetAsync()
        {
            Admins = await _adminRepo.GetAllAsync();
        }
    }
}
