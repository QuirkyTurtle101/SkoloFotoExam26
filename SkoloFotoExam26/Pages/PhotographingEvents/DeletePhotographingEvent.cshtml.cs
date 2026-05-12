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
        public async Task OnGet(int id)
        {
            PhotographingEventDelete = await _photographingEventRepo.GetAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _photographingEventRepo.DeleteAsync(id);
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
