using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Students
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<Student, int> sRepo;
        public List<Student> Students { get; set; }
        public IndexModel(IRepoAsync<Student, int> studentRepo)
        {
            sRepo = studentRepo;
        }
        public async Task OnGetAsync()
        {
            Students = await sRepo.GetAllAsync();
        }
    }
}
