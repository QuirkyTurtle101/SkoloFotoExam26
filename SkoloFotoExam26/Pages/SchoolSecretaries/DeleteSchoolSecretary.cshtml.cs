using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class DeleteSchoolSecretaryModel : PageModel
    {
        private IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        [BindProperty]
        public SchoolSecretary SchoolSecretaryToBeDeleted { get; set; }

        public DeleteSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo)
        {
            _schoolSecRepo = schoolSecRepo;
        }

        public async Task OnGetAsync(int schoolSecretaryID)
        {
            SchoolSecretaryToBeDeleted = await _schoolSecRepo.GetAsync(schoolSecretaryID);
        }

        public async Task<IActionResult> OnPostAsync(int schoolSecretaryID)
        {
            try
            { 
                //await _loginRepo.DeleteAsync(photographToBeDeleted.Email);
                await _schoolSecRepo.DeleteAsync(schoolSecretaryID);
                return RedirectToPage("Index");
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - der er andre data, som er knyttet til denne sekretær";
                SchoolSecretaryToBeDeleted = await _schoolSecRepo.GetAsync(schoolSecretaryID);
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                SchoolSecretaryToBeDeleted = await _schoolSecRepo.GetAsync(schoolSecretaryID);
                return Page();
            }

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
