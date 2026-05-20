using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photographers
{
    public class EditPhotographerModel : PageModel
    {
        IRepoAsync<Photographer, int> _photographerRepo;
        [BindProperty]
        public Photographer PhotographerToUpdate { get; set; }

        public EditPhotographerModel(IRepoAsync<Photographer, int> photographerRepo)
        {
            _photographerRepo = photographerRepo;
        }

        public async void OnGet(int photographerID)
        {
            PhotographerToUpdate = await _photographerRepo.GetAsync(photographerID);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _photographerRepo.UpdateAsync(PhotographerToUpdate);
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl i databasen. Fotograf er ikke blevet redigeret";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = "Fejl ved redigering";
                return Page();
            }
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                _photographerRepo.DeleteAsync(PhotographerToUpdate.ID);
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - fotografen kan ikke slettes, da der er andre data, som er knyttet til denne";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }

            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
