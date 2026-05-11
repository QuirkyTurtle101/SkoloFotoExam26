using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Teachers
{
    public class CreateTeacherModel : PageModel
    {

        private IRepoAsync<Teacher, int> _teacherRepo;

        private IRepoAsync<School, int> _schoolRepo;

        private IRepoAsync<LoginInfo, string> _loginInfoRepo;


        public Teacher NewTeacher { get; set; }

        [BindProperty]
        public int SchoolID { get; set; }
        [BindProperty]
        public string Initials { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Email { get; set; }
        
        public List<School> SchoolList { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public UserType UserType { get; set; }

        public CreateTeacherModel(IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<School, int> schoolRepo, IRepoAsync<LoginInfo, string> loginInfoRepo)
        {
            _teacherRepo = teacherRepo;
            _schoolRepo = schoolRepo;
            _loginInfoRepo = loginInfoRepo;

        }

        public async Task OnGet()
        {
            SchoolList = await _schoolRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            try
            {

                await _loginInfoRepo.AddAsync(new LoginInfo(Email, Password, UserType));

                School school = await _schoolRepo.GetAsync(SchoolID);
                NewTeacher = new Teacher(Initials, FirstName, LastName, PhoneNumber, Email, school);
                await _teacherRepo.AddAsync(NewTeacher);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
