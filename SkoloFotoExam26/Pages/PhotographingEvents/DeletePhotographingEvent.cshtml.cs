using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.PhotographingEvents
{
    public class DeletePhotographingEventModel : PageModel
    {

        private IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        public DeletePhotographingEventModel(IRepoAsync<PhotographingEvent, int> photographingEventRepo)
        {
            _photographingEventRepo = photographingEventRepo;

        }
        [BindProperty]
        public PhotographingEvent PhotographingEventDelete { get; set; }
        public async Task OnGetAsync(int PhotographingEventID)
        {
            PhotographingEventDelete = await _photographingEventRepo.GetAsync(PhotographingEventID);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int PhotographingEventID)
        {
            try
            {
                await _photographingEventRepo.DeleteAsync(PhotographingEventID);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {

                ViewData["Title"] = ex.Message;
                return Page();
            }
        }
    }
}
