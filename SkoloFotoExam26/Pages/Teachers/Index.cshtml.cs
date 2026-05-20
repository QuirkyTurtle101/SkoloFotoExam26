using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.Teachers
{
    public class IndexModel : PageModel
    {

        private TeacherRepoAsync _teacherRepo;

        public List<Teacher> TeacherList { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string FilterCriteria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public IndexModel(IRepoAsync<Teacher, int> teacherRepo)
        {
            _teacherRepo = (TeacherRepoAsync)teacherRepo;
        }
        public async Task OnGetAsync()
        {

                TeacherList = await _teacherRepo.GetAllAsync();

            TeacherList = await _teacherRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                TeacherList = await _teacherRepo.FilterTeachersAsync(FilterCriteria);
            }

            Sorts();
        }

        private void Sorts()
        {
            IEnumerable<Teacher> filter = TeacherList;
            switch (SortBy)
            {
                case "Initials":
                    filter = filter.OrderBy(t => t.Initials);
                    break;
                case "FirstName":
                    filter = filter.OrderBy(t => t.FirstName, StringComparer.OrdinalIgnoreCase);
                    break;
                case "LastName":
                    filter = filter.OrderBy(t => t.LastName, StringComparer.OrdinalIgnoreCase);
                    break;
                case "Email":
                    filter = filter.OrderBy(m => m.Email, StringComparer.OrdinalIgnoreCase);
                    break;
                case "PhoneNumber":
                    filter = filter.OrderBy(m => m.PhoneNumber);
                    break;
            }
            TeacherList = filter.ToList();
        }


    }    
}
