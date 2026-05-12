using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages
{
    public class CreateSchoolSecretaryModel : PageModel
    {
        private IRepoAsync<PhotographingEvent, int> _eventRepo;

        private IRepoAsync<Photographer, int> _photographerRepo;

        private IRepoAsync<SchoolSecretary, int> _secretaryRepo;

        [BindProperty]
        public DateTime Start { get; set; } = DateTime.Today;
        [BindProperty]
        public DateTime End { get; set; } = DateTime.Today;

        [BindProperty]
        public PhotographingEvent NewPhotographingEvent { get; set; }

        [BindProperty]
        public int SchoolSecretaryID { get; set; }

        [BindProperty]
        public int PhotographerID { get; set; }

        //[BindProperty]
        //public SchoolSecretary TheSchoolSecretary { get; set; }

        //[BindProperty]
        //public Photographer ThePhotographer { get; set; }

        public List<SchoolSecretary> SecretaryList { get; set; }

        public List<Photographer> PhotographerList { get; set; }
        

        public CreateSchoolSecretaryModel(IRepoAsync<PhotographingEvent, int> photographingEventRepo, IRepoAsync<Photographer, int> photographerRepo,
            IRepoAsync<SchoolSecretary, int> secretaryRepo)
        {
            _eventRepo = photographingEventRepo;
            _photographerRepo = photographerRepo;
            _secretaryRepo = secretaryRepo;
        }

        public async Task OnGetAsync()
        {
            SecretaryList = await _secretaryRepo.GetAllAsync();
            PhotographerList = await _photographerRepo.GetAllAsync();

        }


        public async Task<IActionResult> OnPostAsync()
        {

            //if (!ModelState.IsValid)
            //    return Page();
            try
            {
                SchoolSecretary schoolSecretary = await _secretaryRepo.GetAsync(SchoolSecretaryID);
                Photographer photographer = await _photographerRepo.GetAsync(PhotographerID);

                PhotographingEvent newPhotoEvent = new PhotographingEvent(Start, End, schoolSecretary, photographer);
                await _eventRepo.AddAsync(newPhotoEvent);

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("Index");

        }

    }


    
}
