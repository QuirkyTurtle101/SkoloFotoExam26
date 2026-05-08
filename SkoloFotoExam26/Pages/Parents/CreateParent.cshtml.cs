using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Parents
{
    public class CreateParentModel : PageModel
    {
        private IRepoAsync<Parent, int> _parentRepo;
        [BindProperty]
        public Parent NewParent { get; set; }
        public CreateParentModel(IRepoAsync<Parent, int> parentRepoAsync)
        {
            _parentRepo = parentRepoAsync;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _parentRepo.AddAsync(NewParent);
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
