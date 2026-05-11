using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class ChooseEventsModel : PageModel
    {
        IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        public List<PhotographingEvent> EventList { get; set; }

        public ChooseEventsModel(IRepoAsync<PhotographingEvent, int> photographingEventRepo)
        {
            _photographingEventRepo = photographingEventRepo;
        }
        public async Task OnGet()
        {
            EventList = await _photographingEventRepo.GetAllAsync();
        }
    }
}
