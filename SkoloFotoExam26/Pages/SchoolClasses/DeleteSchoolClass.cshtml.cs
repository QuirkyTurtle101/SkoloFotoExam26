using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolClasses
{
    public class DeleteSchoolClassModel : PageModel
    {
        private IRepoAsync<SchoolClass, int> _schoolClasses;

        [BindProperty]
        public SchoolClass SchoolClassDelete { get; set; }

        public DeleteSchoolClassModel(IRepoAsync<SchoolClass, int> schoolClasses)
        {
            _schoolClasses = schoolClasses;

        }
        
        public async Task OnGetAsync(int SchoolClassID)
        {
            SchoolClassDelete = await _schoolClasses.GetAsync(SchoolClassID);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int SchoolClassID)
        {
            try
            {
                await _schoolClasses.DeleteAsync(SchoolClassID);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {

                ViewData["Title"] = ex.Message;
                return Page();
            }
        }
    }
}
