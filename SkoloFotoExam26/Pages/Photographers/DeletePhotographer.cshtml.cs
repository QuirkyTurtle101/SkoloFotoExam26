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

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsyncDelete(int photographerID)
        {
            try
            {
                Photographer photographToBeDeleted = await _photographerRepo.GetAsync(photographerID);
                //await _loginRepo.DeleteAsync(photographToBeDeleted.Email);
                await _photographerRepo.DeleteAsync(photographerID);
                return RedirectToPage("Index");
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - der er afhængigheder af denne fotograf i systemet, så denne kan ikke slettes";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }

    }
}
