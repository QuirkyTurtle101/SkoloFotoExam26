using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public async Task OnGetAsync(int ParentID)
        {
            Students = await sRepo.GetAllAsync();
            Parent = await pRepo.GetAsync(ParentID);
        }
    }
}
