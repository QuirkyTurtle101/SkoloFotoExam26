using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Threading.Tasks;

namespace SkoloFotoExam26.Pages.Parents
{
    public class CreateParentModel : PageModel
    {
        private IRepoAsync<Parent, int> _parentRepo;
        private IRepoAsync<LoginInfo, string> _loginInfoRepo;

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
        [BindProperty]
        public List<Parent> Parents { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public UserType UserType { get; set; }


        public CreateParentModel(IRepoAsync<Parent, int> parentRepoAsync, IRepoAsync<LoginInfo, string> loginInfoRepo)
        {
            _parentRepo = parentRepoAsync;
            _loginInfoRepo = loginInfoRepo;
        }

        public async Task OnGet()
        {
            Parents = await _parentRepo.GetAllAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _loginInfoRepo.AddAsync(new LoginInfo(Email, Password, UserType));
                await _parentRepo.AddAsync(new Parent(FirstName, LastName, Email, PhoneNumber, Street, ZipCode, City));
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse, prøv igen";
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
