using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class EditSchoolSecretaryModel : PageModel
    {
        IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        IRepoAsync<School, int> _schoolRepo;
        [BindProperty]
        public SchoolSecretary SecretaryToBeUpdated { get; set; }
        [BindProperty]
        public int ChosenSchool { get; set; }
        [BindProperty]
        public List<School> Schools { get; set; }
        public EditSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo, IRepoAsync<School, int> schoolRepo)
        {
            _schoolSecRepo = schoolSecRepo;
            _schoolRepo = schoolRepo;
        }

        public async Task OnGetAsync(int schoolSecretaryID, int schoolID)
        {

            Schools = await _schoolRepo.GetAllAsync();
            //ChosenSchool = await _schoolRepo.GetAsync(schoolID);
            SecretaryToBeUpdated = await _schoolSecRepo.GetAsync(schoolSecretaryID);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                SchoolSecretary secretaryToBeUpdated = new SchoolSecretary(SecretaryToBeUpdated.ID ,SecretaryToBeUpdated.FirstName, SecretaryToBeUpdated.LastName, SecretaryToBeUpdated.Initials, SecretaryToBeUpdated.PhoneNumber, SecretaryToBeUpdated.Email, await _schoolRepo.GetAsync(ChosenSchool));

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

        public async Task<IActionResult> OnPostDeleteAsync()
        {

            await _schoolSecRepo.DeleteAsync(SecretaryToBeUpdated.ID);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
