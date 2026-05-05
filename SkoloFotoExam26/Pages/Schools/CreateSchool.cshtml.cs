using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Schools
{
    public class CreateSchoolModel : PageModel
    {

        private ISchoolRepoAsync _repoAsync;

        [BindProperty]
        public School NewSchool { get; set; }

        public CreateSchoolModel(ISchoolRepoAsync repoAsync)
        {
            _repoAsync = repoAsync;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _repoAsync.AddAsync(NewSchool);
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = sqlex.Message;
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
