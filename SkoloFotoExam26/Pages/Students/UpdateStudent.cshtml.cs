using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Students
{
    public class UpdateStudentModel : PageModel
    {
        IRepoAsync<Student, int> _studentRepo;
        IRepoAsync<Parent, int> _parentRepo;
        IRepoAsync<School, int> _schoolRepo;
        IRepoAsync<SchoolClass, int> _schoolClassRepo;

        [BindProperty]
        public Student StudentToUpdate { get; set; }
        [BindProperty]
        public int ParentID { get; set; }
        [BindProperty]
        public int SchoolID { get; set; }
        [BindProperty]
        public int SchoolClassID { get; set; }
        //public Parent ParentIDToUpdate { get; set; }
        //public School SchoolIDToUpdate { get; set; }
        //public SchoolClass SchoolClassIDToUpdate { get; set; }
        public List <Parent> ParentList { get; set; }
        public List <School> SchoolList { get; set; }
        public List <SchoolClass> SchoolClassList { get; set; }
        
        public UpdateStudentModel(IRepoAsync<Student, int> studentRepo, IRepoAsync<Parent, int> parentRepo, IRepoAsync<School, int> schoolRepo, IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            _studentRepo = studentRepo;
            _parentRepo = parentRepo;
            _schoolRepo = schoolRepo;
            _schoolClassRepo = schoolClassRepo;
        }

        public async Task OnGetAsync(int StudentID)
        {
            StudentToUpdate = await _studentRepo.GetAsync(StudentID);
            ParentList = await _parentRepo.GetAllAsync();
            SchoolList = await _schoolRepo.GetAllAsync();
            SchoolClassList = await _schoolClassRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _studentRepo.UpdateAsync(StudentToUpdate);
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Opdatering mislykkes";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                _studentRepo.DeleteAsync(StudentToUpdate.StudentID);
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved sletning - barnet kan ikke slettes, da der er andre data, som er knyttet til denne";
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
