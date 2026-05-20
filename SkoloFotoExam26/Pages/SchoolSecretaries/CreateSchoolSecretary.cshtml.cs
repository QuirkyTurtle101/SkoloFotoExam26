using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class CreateSchoolSecretaryModel : PageModel
    {

        private IRepoAsync<SchoolSecretary, int> _schoolSecRepo;
        private IRepoAsync<School, int> _schoolRepo;
        private IRepoAsync<LoginInfo, string> _loginInfoRepo;

        //[BindProperty]
        //public SchoolSecretary NewSchoolSecretary { get; set; }
        [BindProperty]
        public int ChosenSchoolID { get; set; }
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
        [BindProperty]
        public List<School> Schools { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public UserType UserType { get; set; }


        public CreateSchoolSecretaryModel(IRepoAsync<SchoolSecretary, int> schoolSecretaryAsync, IRepoAsync<School, int> schoolRepoAsync, IRepoAsync<LoginInfo, string> loginInfoRepo)
        {
            _schoolSecRepo = schoolSecretaryAsync;
            _schoolRepo = schoolRepoAsync;
            _loginInfoRepo = loginInfoRepo;
        }

        public async Task OnGet()
        {
            Schools = await _schoolRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _loginInfoRepo.AddAsync(new LoginInfo(Email, Password, UserType));

                await _schoolSecRepo.AddAsync(new SchoolSecretary(FirstName,LastName,Initials,PhoneNumber,Email, await _schoolRepo.GetAsync(ChosenSchoolID)));
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse, prøv igen.";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Fejl ved opprettelse";
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
