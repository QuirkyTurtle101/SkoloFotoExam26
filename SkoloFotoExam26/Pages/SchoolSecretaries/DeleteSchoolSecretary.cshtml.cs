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
        IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        IRepoAsync<LoginInfo, string> _loginRepo;

        [BindProperty]
        public SchoolSecretary SchoolSecretaryToBeDeleted { get; set; }

        public DeleteSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo, IRepoAsync<LoginInfo, string> loginRepo)
        {
            _schoolSecRepo = schoolSecRepo;
            _loginRepo = loginRepo;

        }
        public async Task OnGetAsync(int schoolSecretaryID)
        {
            SchoolSecretaryToBeDeleted = await _schoolSecRepo.GetAsync(schoolSecretaryID);
        }

        public async Task<IActionResult> OnPostaAyncDelete(int schoolSecretaryID)//Kan ikke teste før loginRepo
        {
            try
            {
                SchoolSecretary schoolSecretaryToBeDeleted = await _schoolSecRepo.GetAsync(schoolSecretaryID);

                //await _loginRepo.DeleteAsync(schoolSecretaryToBeDeleted.Email);
                await _schoolSecRepo.DeleteAsync(schoolSecretaryID);
                return RedirectToPage("Index");
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Denne kan ikke slettes, da der er andre data, som er knyttet til denne";
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
