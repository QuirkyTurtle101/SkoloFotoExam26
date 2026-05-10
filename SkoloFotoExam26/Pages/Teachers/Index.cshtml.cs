using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Teachers
{
    public class IndexModel : PageModel
    {

        private IRepoAsync<Teacher, int> _teacherRepo;

        public List<Teacher> TeacherList { get; set; }

        public IndexModel(IRepoAsync<Teacher, int> teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }
        public async Task OnGetAsync()
        {
            TeacherList = await _teacherRepo.GetAllAsync();
        }

    }
}
