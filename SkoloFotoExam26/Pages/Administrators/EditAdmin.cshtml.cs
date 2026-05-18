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
                ViewData["ErrorMessage"] = "Kunne ikke redigeres";

                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDelete(int adminID)
        {
            try
            {
                _adminRepo.DeleteAsync(adminID);
                return RedirectToPage("Index");
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - der er andre data, som er knyttet til denne administrator";
                AdminToBeUpdated = await _adminRepo.GetAsync(adminID);
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                AdminToBeUpdated = await _adminRepo.GetAsync(adminID);
                return Page();
            }
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }


    }

    
}
