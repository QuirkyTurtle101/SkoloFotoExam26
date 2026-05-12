
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.Schools
{
    public class DeleteSchoolModel : PageModel
    {
        IRepoAsync<School, int> _schoolRepo;

        [BindProperty]
        public School SchoolToBeDeleted { get; set; }
        public DeleteSchoolModel(IRepoAsync<School, int> schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }

        public async Task<IActionResult> OnGet(int schoolID)
        {
            SchoolToBeDeleted = await _schoolRepo.GetAsync(schoolID);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int schoolID)
        {
            try
            {
                await _schoolRepo.DeleteAsync(schoolID);
                return RedirectToPage("index");
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - der er andre data, som er knyttet til denne skole";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
