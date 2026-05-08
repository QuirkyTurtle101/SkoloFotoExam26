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
        public int ParentID { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Street { get; set; }
        [BindProperty]
        public int ZipCode { get; set; }
        [BindProperty]
        public string City { get; set; }


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
                await _parentRepo.AddAsync(new Parent(FirstName, LastName, Email, PhoneNumber, Street, ZipCode, City));
                //await _parentRepo.GetAsync(ParentID)
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
