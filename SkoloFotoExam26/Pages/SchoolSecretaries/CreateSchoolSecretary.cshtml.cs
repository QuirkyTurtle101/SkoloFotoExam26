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
        private IRepoAsync<School, int> _schoolRepo;

        //[BindProperty]
        //public SchoolSecretary NewSchoolSecretary { get; set; }
        [BindProperty]
        public int SchoolID { get; set; }// skal formentlig ændres i fremtiden.
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Initials { get; set; }

        public CreateSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecretaryAsync, IRepoAsync<School, int> schoolRepoAsync)
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

                await _schoolSecRepo.AddAsync(new SchoolSecretary(FirstName,LastName,Initials,PhoneNumber,Email, await _schoolRepo.GetAsync(SchoolID)));
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "fejl ved oprettelse, prøv igen.";
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
