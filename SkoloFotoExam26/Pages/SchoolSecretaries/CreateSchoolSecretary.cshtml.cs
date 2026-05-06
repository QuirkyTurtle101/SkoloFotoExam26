using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class CreateSchoolSecretaryModel : PageModel
    {
        private ISchoolSecretaryAsync _schoolSecRepo;

        [BindProperty]
        public SchoolSecretary NewSchoolSecretary { get; set; }

        public CreateSchoolSecretaryModel(ISchoolSecretaryAsync schoolSecretaryAsync)
        {
            _schoolSecRepo = schoolSecretaryAsync;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
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
