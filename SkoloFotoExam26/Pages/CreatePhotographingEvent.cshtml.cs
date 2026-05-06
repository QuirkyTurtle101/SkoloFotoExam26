using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages
{
    public class CreateSchoolSecretaryModel : PageModel
    {
        private IPhotographingEventRepoAsync _repo;
        [BindProperty]
        public PhotographingEvent NewPhotographingEvent { get; set; }

        [BindProperty]
        public int SchoolSecretaryID { get; set; }

        [BindProperty]
        public int PhotographerID { get; set; }

        public CreateSchoolSecretaryModel(IPhotographingEventRepoAsync photographingEventRepo)
        {
            _repo = photographingEventRepo;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
                return Page();
            try
            {
                //PhotographingEvent newPhotographingEvent = new PhotographingEvent(NewPhotographingEvent.Start,
                //    NewPhotographingEvent.End, _repo.GetAsync(SchoolSecretaryID), _repo.GetAsync(PhotographerID);

                //NewPhotographingEvent = newPhotographingEvent;
                await _repo.AddAsync(NewPhotographingEvent);

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
        
    }


    
}
