using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Teachers
{
    public class CreateTeacherModel : PageModel
    {

        private ITeacherRepoAsync _teacherRepo;

        private ISchoolRepoAsync _schoolRepo;

        [BindProperty]
        public Teacher NewTeacher { get; set; }

        [BindProperty]
        public int SchoolID { get; set; }

        public List<School> SchoolList; 

        public CreateTeacherModel(ITeacherRepoAsync teacherRepo, ISchoolRepoAsync schoolRepo)
        {
            _teacherRepo = teacherRepo;
            _schoolRepo = schoolRepo;
        }

        public async Task OnGet()
        {
            SchoolList = await _schoolRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                School school = await _schoolRepo.GetAsync(SchoolID);
                Teacher newTeacher = new Teacher(NewTeacher.TeacherID, NewTeacher.Initials, NewTeacher.FirstName, 
                    NewTeacher.LastName, NewTeacher.PhoneNumber, NewTeacher.Email, school);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
