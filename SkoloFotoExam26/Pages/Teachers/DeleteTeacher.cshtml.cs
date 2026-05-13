using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
namespace SkoloFotoExam26.Pages.Teachers;


    public class DeleteTeacherModel : PageModel
    {
        private IRepoAsync<Teacher, int> _teachers;

        [BindProperty]
        public Teacher TeacherDelete { get; set; }

        public DeleteTeacherModel(IRepoAsync<Teacher, int> teachers)
        {
            _teachers = teachers;

        }
        public async Task OnGetAsync(int ID)
        {
            TeacherDelete = await _teachers.GetAsync(ID);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int ID)
        {
            try
            {
                await _teachers.DeleteAsync(ID);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {

                ViewData["Title"] = ex.Message;
                return Page();
            }
        }
    }

