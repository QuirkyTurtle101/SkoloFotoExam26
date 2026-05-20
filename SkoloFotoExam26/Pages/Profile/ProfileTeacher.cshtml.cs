using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Profile
{
    public class ProfileTeacherModel : PageModel
    {
        private IRepoAsync<Teacher, int> _teacherRepo;
        private IRepoAsync<Student, int> _studentRepo;
        public Teacher TheTeacher { get; set; }
        public List<Student> Students { get; set; }
        public ProfileTeacherModel(IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<Student, int> studentRepo)
        {
            _teacherRepo = teacherRepo;
            _studentRepo = studentRepo;
        }
        public async Task OnGetAsync()
        {
            TheTeacher = await _teacherRepo.GetAsync((int)HttpContext.Session.GetInt32("UserID"));
            List<Student> temp = await _studentRepo.GetAllAsync();
            Students = new List<Student>();
            foreach (Student student in temp)
            {
                if (student.School.SchoolID == TheTeacher.TheSchool.SchoolID)
                {
                    Students.Add(student);
                }
            }
        }
    }
}
