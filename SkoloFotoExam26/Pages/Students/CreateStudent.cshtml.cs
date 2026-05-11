using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Students
{
    public class CreateStudentModel : PageModel
    {
        private IRepoAsync<Student, int> _studentRepo;
        private IRepoAsync<Parent, int> _parentRepo;
        private IRepoAsync<School, int> _schoolRepo;
        private IRepoAsync<SchoolClass, int> _schoolClassRepo;

        [BindProperty]
        public int StudentID { get; set; }
        [BindProperty]
        public Student NewStudent { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string MiddleName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public int ParentID { get; set; }
        [BindProperty]
        public int SchoolID { get; set; }
        [BindProperty]
        public int SchoolClassID { get; set; }
        [BindProperty]
        public List<Student> StudentList { get; set; }
        [BindProperty]
        public List<Parent> ParentList { get; set; }
        [BindProperty]
        public List<School> SchoolList { get; set; }
        [BindProperty]
        public List<SchoolClass> SchoolClassList { get; set; }
  
        public CreateStudentModel(IRepoAsync<Student, int> studentRepoAsync, IRepoAsync<Parent, int> parentRepoAsync, IRepoAsync<School, int> schoolRepoAsync, IRepoAsync<SchoolClass, int> schoolClassRepoAsync)
        {
            _studentRepo = studentRepoAsync;
            _parentRepo = parentRepoAsync;
            _schoolRepo = schoolRepoAsync;
            _schoolClassRepo = schoolClassRepoAsync;
        }

        public async Task OnGetAsync()
        {
            ParentList = await _parentRepo.GetAllAsync();
            SchoolList = await _schoolRepo.GetAllAsync();
            SchoolClassList = await _schoolClassRepo.GetAllAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Parent parent = await _parentRepo.GetAsync(ParentID);
                School school = await _schoolRepo.GetAsync(SchoolID);
                SchoolClass schoolClass = await _schoolClassRepo.GetAsync(SchoolClassID);
                Student newStudent = new Student(0, NewStudent.FirstName, NewStudent.MiddleName, NewStudent.LastName, parent, school, schoolClass);
                await _studentRepo.AddAsync(newStudent);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
