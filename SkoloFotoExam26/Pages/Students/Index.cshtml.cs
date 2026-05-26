using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.Students
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<Parent, int> pRepo;
        private IRepoAsync<Student, int> sRepo;
        public List<Student> Students { get; set; }
        public int ParentID { get; set; }
        public Parent Parent { get; set; }
        public IndexModel(IRepoAsync<Student, int> studentRepo, IRepoAsync<Parent, int> parentRepo)
        {
            sRepo = studentRepo;
            pRepo = parentRepo;
        }

        public async Task<IActionResult> OnGetAsync(int ParentID)
        {
            Students = new List<Student>();
            try
            {
                List<Student> ListOfStudents = await sRepo.GetAllAsync();
                foreach (Student s in ListOfStudents)
                {
                    if (s.Parent.ID == ParentID)
                    {
                        Students.Add(s);
                    }
                }
                if (Students.Count == 0)
                {
                    ViewData["ErrorMessage"] = "Ingen børn at hente";
                    Parent = await pRepo.GetAsync(ParentID);
                    return Page();
                }
                Parent = await pRepo.GetAsync(ParentID);
            }
            catch (SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "Fejl ved hentning, prøv igen";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return Page();
        }
    }
}
