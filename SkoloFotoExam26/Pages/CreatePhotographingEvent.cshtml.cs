using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages
{
    public class CreateSchoolSecretaryModel : PageModel
    {
        private IPhotographingEvent _repo;
        [BindProperty]
        public PhotographingEvent NewPhotographingEvent { get; set; }

        public CreateSchoolSecretaryModel(IPhotographingEvent photographingEventRepo)
        {
            _repo = photographingEventRepo;
        }
        public void OnGet()
        {

        }
    }
}
