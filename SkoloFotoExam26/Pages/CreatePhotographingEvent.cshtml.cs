using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages
{
    public class CreateSchoolSecretaryModel : PageModel
    {
        private IPhotographingEventRepoAsync _eventRepo;

        private IRepoAsync<Photographer, int> _photographerRepo;

        private IRepoAsync<SchoolSecretary, int> _secretaryRepo;

        [BindProperty]
        public PhotographingEvent NewPhotographingEvent { get; set; }

        [BindProperty]
        public int SchoolSecretaryID { get; set; }

        [BindProperty]
        public int PhotographerID { get; set; }

        public CreateSchoolSecretaryModel(IPhotographingEventRepoAsync photographingEventRepo, IRepoAsync<Photographer, int> photographerRepo,
            IRepoAsync<SchoolSecretary, int> secretaryRepo)
        {
            _eventRepo = photographingEventRepo;
            _photographerRepo = photographerRepo;
            _secretaryRepo = secretaryRepo;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
                return Page();
            try
            {
                PhotographingEvent newPhotographingEvent = new PhotographingEvent(NewPhotographingEvent.Start,
                    NewPhotographingEvent.End, await _secretaryRepo.GetAsync(SchoolSecretaryID), 
                    await _photographerRepo.GetAsync(PhotographerID));

                NewPhotographingEvent = newPhotographingEvent;
                await _eventRepo.AddAsync(NewPhotographingEvent);

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
        
    }


    
}
