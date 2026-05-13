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
        }

        public async Task<IActionResult> OnPostUpdate(int photographingEventID)
        {
            try
            {
                //PhotographingEventToEdit = await _photographingEvents.GetAsync(photographingEventID);
                await _photographingEvents.UpdateAsync(PhotographingEventToEdit);
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
