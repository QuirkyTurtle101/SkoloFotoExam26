using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Schools
{
    public class CreateSchoolModel : PageModel
    {

        private IRepoAsync<School, int> _schoolRepo;

        [BindProperty]
        public School NewSchool { get; set; }

        public CreateSchoolModel(IRepoAsync<School, int> schoolRepoAsync)
        {
            _schoolRepo = schoolRepoAsync;
        }
        
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _schoolRepo.AddAsync(NewSchool);
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse";
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
