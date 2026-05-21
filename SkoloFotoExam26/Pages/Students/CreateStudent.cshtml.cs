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


        public async Task OnGetAsync(int ParentID)
        {
            this.ParentID = ParentID;

            // Hent alle klasser fra databasen, så vi kan vise dem i dropdown'en
            SchoolClassList = await _schoolClassRepo.GetAllAsync();
            SchoolList = await _schoolRepo.GetAllAsync();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // 1. Opret et Student objekt manuelt ud fra de properties, formen har udfyldt
                // Vi bruger ParentID fra formen og SchoolClassID (husk at tilføje den til formen!)
                Student studentToSave = new Student
                {
                    FirstName = this.FirstName,
                    MiddleName = this.MiddleName,
                    LastName = this.LastName,
                    Parent = new Parent { ID = this.ParentID },
                    School = new School {SchoolID = this.SchoolID},
                    SchoolClass = new SchoolClass { SchoolClassID = this.SchoolClassID }
                };

                // 2. Gem via repo
                await _studentRepo.AddAsync(studentToSave);

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Fejl ved oprettelse: " + ex.Message;
                SchoolList = await _schoolRepo.GetAllAsync();
                SchoolClassList = await _schoolClassRepo.GetAllAsync();
                return Page();
            }
        }
    }
}
