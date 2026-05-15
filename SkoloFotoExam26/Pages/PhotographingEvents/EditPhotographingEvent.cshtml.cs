using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Runtime.CompilerServices;

namespace SkoloFotoExam26.Pages.PhotographingEvents
{
    public class EditPhotographingEventModel : PageModel
    {
        private IRepoAsync<PhotographingEvent, int> _photographingEvents;

        IRepoAsync<Photographer, int> _photographers;

        private IRepoAsync<SchoolSecretary, int> _secretaryRepo;


        [BindProperty]
        public PhotographingEvent PhotographingEventToEdit { get; set; }

        [BindProperty]
        public int PhotographerID { get; set; }

        [BindProperty]
        public int SchoolSecretaryID { get; set; }

        [BindProperty]
        public DateTime Start { get; set; } = DateTime.Today;

        [BindProperty]
        public DateTime End { get; set; } = DateTime.Today;
        public List<Photographer> PhotographerList { get; set; }

        public List<SchoolSecretary> SecretaryList { get; set; }
        public EditPhotographingEventModel(IRepoAsync<PhotographingEvent, int> photographingEvents, IRepoAsync<Photographer, int>
            photographers, IRepoAsync<SchoolSecretary, int> secretaryRepo)
        {
            _photographingEvents = photographingEvents;
            _photographers = photographers;
            _secretaryRepo = secretaryRepo;
        }
        public async Task OnGet(int photographingEventID)
        {
            PhotographingEventToEdit = await _photographingEvents.GetAsync(photographingEventID);
            PhotographerList = await _photographers.GetAllAsync();
            SecretaryList = await _secretaryRepo.GetAllAsync();
            PhotographerID = PhotographingEventToEdit.Photographer.ID;
            SchoolSecretaryID = PhotographingEventToEdit.SchoolSecretary.ID;
            Start = PhotographingEventToEdit.Start;
            End = PhotographingEventToEdit.End;
        }

        public async Task<IActionResult> OnPostUpdate(int photographingEventID)
        {
            try
            {
                PhotographerList = await _photographers.GetAllAsync();
                SecretaryList = await _secretaryRepo.GetAllAsync();
                //PhotographingEventToEdit = await _photographingEvents.GetAsync(photographingEventID);
                PhotographingEvent edited = new PhotographingEvent(photographingEventID, Start, End, 
                    await _secretaryRepo.GetAsync(SchoolSecretaryID), 
                    await _photographers.GetAsync(PhotographerID));
                    await _photographingEvents.UpdateAsync(edited);
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
