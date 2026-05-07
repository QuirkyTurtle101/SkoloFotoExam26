using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class CreateSchoolSecretaryModel : PageModel
    {

        private IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        private ISchoolRepoAsync _schoolRepo;

        [BindProperty]
        public SchoolSecretary NewSchoolSecretary { get; set; }
        [BindProperty]
        public int SchoolID { get; set; }// skal formentlig ændres i fremtiden.

        public CreateSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecretaryAsync, ISchoolRepoAsync schoolRepoAsync)
        {
            _schoolSecRepo = schoolSecretaryAsync;
            _schoolRepo = schoolRepoAsync;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //School
                await _schoolSecRepo.AddAsync(NewSchoolSecretary);
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = sqlex.Message;
                return Page();
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
