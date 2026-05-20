using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photographers
{
    public class DeletePhotographerModel : PageModel
    {
        IRepoAsync<Photographer, int> _photographerRepo;
        IRepoAsync<LoginInfo, string> _loginRepo;


        public Photographer PhotographerToBeDeleted { get; set; }

        public DeletePhotographerModel(IRepoAsync<Photographer, int> photographerRepo, IRepoAsync<LoginInfo, string>loginRepo)
        {
            _photographerRepo = photographerRepo;
            _loginRepo = loginRepo;
        }

        public async Task OnGetAsync(int photographerID)
        {
            PhotographerToBeDeleted = await _photographerRepo.GetAsync(photographerID);
        }

        public async Task<IActionResult> OnPostAsync(int photographerID)
        {
            try
            {
                Photographer photographToBeDeleted = await _photographerRepo.GetAsync(photographerID);
                //await _loginRepo.DeleteAsync(photographToBeDeleted.Email);
                //Photographer photographToBeDeleted = new Photographer(PhotographerToBeDeleted.ID, PhotographerToBeDeleted.FirstName, PhotographerToBeDeleted.LastName, PhotographerToBeDeleted.PhoneNumber, PhotographerToBeDeleted.Email, PhotographerToBeDeleted.Website, PhotographerToBeDeleted.CVRNumber, PhotographerToBeDeleted.City, PhotographerToBeDeleted.ZipCode, PhotographerToBeDeleted.Street, PhotographerToBeDeleted.ExperienceInYears, PhotographerToBeDeleted.MaxTravelRadiusInKm, PhotographerToBeDeleted.Instagram, PhotographerToBeDeleted.Facebook);

                await _photographerRepo.DeleteAsync(photographerID);
                return RedirectToPage("Index");
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - fotografen kan ikke slettes, da der er andre data, som er knyttet til denne";
                PhotographerToBeDeleted = await _photographerRepo.GetAsync(photographerID);
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                PhotographerToBeDeleted = await _photographerRepo.GetAsync(photographerID);
                return Page();
            }
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
