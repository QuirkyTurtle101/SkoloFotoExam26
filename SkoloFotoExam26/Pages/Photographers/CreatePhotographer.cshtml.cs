using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photographers
{
    public class CreatePhotographerModel : PageModel
    {
        private IRepoAsync<Photographer, int> _photographerRepo;
        private IRepoAsync<LoginInfo, string> _loginInfoRepo;


        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Website { get; set; }
        [BindProperty]
        public string CVRNumber { get; set; }
        [BindProperty]
        public string Street { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public int ZipCode { get; set; }
        [BindProperty]
        public int ExperienceInYears { get; set; }
        [BindProperty]
        public int MaxTravelRadiusInKm { get; set; }
        [BindProperty]
        public string Instagram { get; set; }
        [BindProperty]
        public string Facebook { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public UserType UserType { get; set; }


        public CreatePhotographerModel(IRepoAsync<Photographer, int> photographerRepoAsync, IRepoAsync<LoginInfo, string> loginInfoRepo)
        {
            _photographerRepo = photographerRepoAsync;
            _loginInfoRepo = loginInfoRepo;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _loginInfoRepo.AddAsync(new LoginInfo(Email, Password, UserType));
                await _photographerRepo.AddAsync(new Photographer(FirstName, LastName, PhoneNumber, Email, Website, CVRNumber, City, ZipCode, Street, ExperienceInYears, MaxTravelRadiusInKm, Instagram, Facebook));
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse, prøv igen.";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse";
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
