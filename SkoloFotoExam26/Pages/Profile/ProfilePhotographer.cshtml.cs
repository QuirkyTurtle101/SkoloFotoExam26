using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Profile
{
    public class ProfilePhotographerModel : PageModel
    {
        private IRepoAsync<Photographer, int> _photographerRepo;
        public Photographer ThePhotographer { get; set; }
        public ProfilePhotographerModel(IRepoAsync<Photographer, int> photographerRepo)
        {
            _photographerRepo = photographerRepo;
        }
        public async Task OnGetAsync()
        {
            ThePhotographer = await _photographerRepo.GetAsync((int)HttpContext.Session.GetInt32("UserID"));
        }
    }
}
