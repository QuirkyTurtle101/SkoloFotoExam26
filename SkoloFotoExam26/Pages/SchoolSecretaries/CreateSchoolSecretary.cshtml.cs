using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class CreateSchoolSecretaryModel : PageModel
    {

        private ISchoolSecretaryRepoAsync _schoolSecRepo;
        private ISchoolRepoAsync _schoolRepo;

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


        public CreateSchoolSecretaryModel(ISchoolSecretaryRepoAsync schoolSecretaryAsync, ISchoolRepoAsync schoolRepoAsync)
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
                SchoolSecretary newSchoolSecretary = new SchoolSecretary(FirstName, LastName, Initials, PhoneNumber, Email, await _schoolRepo.GetAsync(SchoolID));
                //NewSchoolSecretary = newSchoolSecretary;
                await _schoolSecRepo.AddAsync(newSchoolSecretary);
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
