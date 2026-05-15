using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Teachers
{
    public class EditTeacherModel : PageModel
    {
        private IRepoAsync<Teacher, int> _teacherRepo;

        private IRepoAsync<School, int> _schoolRepo;

        //private IRepoAsync<LoginInfo, string> _loginInfoRepo;

        [BindProperty]
        public Teacher TeacherEdit { get; set; }

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

        //[BindProperty]
        //public string Password { get; set; }
        //[BindProperty]
        //public UserType UserType { get; set; }

        public EditTeacherModel(IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<School, int> schoolRepo/*, IRepoAsync<LoginInfo, string> loginInfoRepo*/)
        {
            _teacherRepo = teacherRepo;
            _schoolRepo = schoolRepo;
            //_loginInfoRepo = loginInfoRepo;

        }
        public async Task OnGet(int ID)
        {
            SchoolList = await _schoolRepo.GetAllAsync();
            TeacherEdit = await _teacherRepo.GetAsync(ID);
            SchoolID = TeacherEdit.TheSchool.SchoolID;
            Initials = TeacherEdit.Initials;
            FirstName = TeacherEdit.FirstName;
            LastName = TeacherEdit.LastName;
            PhoneNumber = TeacherEdit.PhoneNumber;
            Email = TeacherEdit.Email;
        }

        public async Task<IActionResult> OnPostUpdate(int ID)
        {
            try
            {
                SchoolList = await _schoolRepo.GetAllAsync();

                Teacher edited = new Teacher(ID, Initials, FirstName, LastName, PhoneNumber, Email, 
                    await _schoolRepo.GetAsync(SchoolID));
                await _teacherRepo.UpdateAsync(edited);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ViewData["Title"] = ex.Message;
                return Page();
            }
        }
    }
}

