using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class EditSchoolSecretaryModel : PageModel
    {
        IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        IRepoAsync<School, int> _schoolRepo;
        public SchoolSecretary SecretaryToBeUpdated { get; set; }
        public School ChosenSchoolID { get; set; }
        public List<School> Schools { get; set; }
        public EditSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo, IRepoAsync<School, int> schoolRepo)
        {
            _schoolSecRepo = schoolSecRepo;
            _schoolRepo = schoolRepo;
        }

        public async Task OnGetAsync(int schoolSecretaryID)
        {
            SecretaryToBeUpdated = await _schoolSecRepo.GetAsync(schoolSecretaryID);
            Schools = await _schoolRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsyncUpdate(int schoolSecretaryID)
        {
            try
            {
                SchoolSecretary secretaryToBeUpdated = await _schoolSecRepo.GetAsync(schoolSecretaryID);
                _schoolSecRepo.UpdateAsync(secretaryToBeUpdated);
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
