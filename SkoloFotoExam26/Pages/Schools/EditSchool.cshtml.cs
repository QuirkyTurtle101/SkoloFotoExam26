using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Schools
{
    public class EditSchoolModel : PageModel
    {
        IRepoAsync<School, int> _schoolRepo;
        [BindProperty]
        public School SchoolToBeUpdated { get; set; }

        public EditSchoolModel(IRepoAsync<School, int> schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }
        public async Task OnGet(int schoolID)
        {
            SchoolToBeUpdated = await _schoolRepo.GetAsync(schoolID);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _schoolRepo.UpdateAsync(SchoolToBeUpdated);
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

        //public async Task<IActionResult> OnPostAsyncDelete()
        //{
        //    try
        //    {
        //        await _schoolRepo.DeleteAsync(SchoolToBeUpdated.SchoolID);
        //    }
        //    catch(SqlException sqlex)
        //    {
        //        ViewData["ErrorMessage"] = "Fejl ved sletning - der er andre data, som er knyttet til denne skole";
        //        return Page();
        //    }
        //    catch(Exception ex)
        //    {
        //        ViewData["ErrorMessage"] = ex.Message;
        //        return Page();
        //    }
        //    return RedirectToPage("Index");
        //}

        public async Task<IActionResult> OnPostAsyncCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
