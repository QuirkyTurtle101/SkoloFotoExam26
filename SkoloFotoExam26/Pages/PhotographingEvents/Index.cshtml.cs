using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.PhotographingEvents
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        public List<PhotographingEvent> PhotographingEvents { get; set; }

        public IndexModel(IRepoAsync<PhotographingEvent, int> photographingEventRepo)
        {
            _photographingEventRepo = photographingEventRepo;
        }
        public async Task OnGet()
        {
            PhotographingEvents = await _photographingEventRepo.GetAllAsync();
        }
    }
}
