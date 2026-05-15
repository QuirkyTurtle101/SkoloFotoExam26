using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Administrators
{
    public class EditAdminModel : PageModel
    {
        IRepoAsync<Administrator, int> _adminRepo;
        [BindProperty]
        public Administrator AdminToBeUpdated { get; set; }

        public EditAdminModel(IRepoAsync<Administrator, int> adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task OnGet(int adminID)
        {
            AdminToBeUpdated = await _adminRepo.GetAsync(adminID);
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _adminRepo.UpdateAsync(AdminToBeUpdated);
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Kunne ikke opdateres";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
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
