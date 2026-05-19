using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Administrators
{
    public class CreateAdminModel : PageModel
    {
        IRepoAsync<Administrator, int> _adminRepo;
        IRepoAsync<LoginInfo, string> _loginRepo;
        //[BindProperty]
        //public Administrator NewAdmin { get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public UserType UserType { get; set; }
        public CreateAdminModel(IRepoAsync<Administrator, int> adminRepo, IRepoAsync<LoginInfo, string> loginRepo)
        {
            _adminRepo = adminRepo;
            _loginRepo = loginRepo;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _loginRepo.AddAsync(new LoginInfo(Email, Password, UserType));
                Administrator admin = new Administrator(FirstName, LastName, PhoneNumber, Email);
                await _adminRepo.AddAsync(admin);

            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl i databasen ved oprettelse";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse";
                return Page();
            }
            return RedirectToPage("Index");

        }
    }
}
