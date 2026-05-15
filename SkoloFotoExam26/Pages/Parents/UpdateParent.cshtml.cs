using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Parents
{
    public class UpdateParentModel : PageModel
    {
        IRepoAsync<Parent, int> _parentRepo;
        [BindProperty]
        public Parent ParentToUpdate { get; set; }
        public UpdateParentModel(IRepoAsync<Parent, int> parentRepo)
        {
            _parentRepo = parentRepo;
        }

        public async Task OnGet(int ParentID)
        {
            ParentToUpdate = await _parentRepo.GetAsync(ParentID);
        }

        public async Task <IActionResult> OnPostAsync()
        {
            try
            {
                await _parentRepo.UpdateAsync(ParentToUpdate);
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Opdatering mislykkes";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                _parentRepo.DeleteAsync(ParentToUpdate.ID);
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - forælderen kan ikke slettes, da der er andre data, som er knyttet til denne";
                return Page();
            }
            catch (Exception ex)
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
