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
        public Photographer PhotographerToUpdate { get; set; }

        public EditPhotographerModel(IRepoAsync<Photographer, int> photographerRepo)
        {
            _photographerRepo = photographerRepo;
        }

        public async void OnGet(int photographerID)
        {
            PhotographerToUpdate = await _photographerRepo.GetAsync(photographerID);
        }

        public async Task<IActionResult> OnPostAsyncUpdate(int photographerID)
        {
            try
            {
                Photographer photographerToUpdate = await _photographerRepo.GetAsync(photographerID);

                await _photographerRepo.UpdateAsync(photographerToUpdate);
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Kunne ikke opdateres";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }

            return RedirectToPage("Index");

        }

    }
}
