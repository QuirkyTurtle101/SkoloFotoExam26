using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Threading.Tasks;

namespace SkoloFotoExam26.Pages.Administrators
{
    public class DeleteAdminModel : PageModel
    {
        IRepoAsync<Administrator, int> _adminRepo;
        public Administrator AdminToBeDeleted { get; set; }

        public DeleteAdminModel(IRepoAsync<Administrator, int> adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task OnGet(int adminID)
        {
            AdminToBeDeleted = await _adminRepo.GetAsync(adminID);
        }

        public async Task<IActionResult> OnPost(int adminID)
        {
            try
            {
                _adminRepo.DeleteAsync(adminID);
                return RedirectToPage("Index");
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - der er andre data, som er knyttet til denne administrator";
                AdminToBeDeleted = await _adminRepo.GetAsync(adminID);
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                AdminToBeDeleted = await _adminRepo.GetAsync(adminID);
                return Page();
            }
        }


    }
}
