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

        IFilterFunction _filterFunction;

        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public IndexModel(IRepoAsync<Teacher, int> teacherRepo, IFilterFunction filterFunction)
        {
            _teacherRepo = (TeacherRepoAsync)teacherRepo;
            _filterFunction = filterFunction;
        }
        public async Task OnGetAsync()
        {

            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                var listOfPreds = FilterPhotographersByPredictes();
                TeacherList = _filterFunction.FilterFunc(await _teacherRepo.GetAllAsync(), listOfPreds);
            }
            else
            {
                TeacherList = await _teacherRepo.GetAllAsync();
            }
        }

        public List<Predicate<Teacher>> FilterPhotographersByPredictes()
        {
            List<Predicate<Teacher>> listOfPredicates = new List<Predicate<Teacher>>();

            Predicate<Teacher> initials = p => p.Initials.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Teacher> firstName = p => p.FirstName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Teacher> lastName = p => p.LastName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Teacher> email = p => p.Email.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Teacher> phoneNumber = p => p.PhoneNumber.ToLower().Contains(FilterCriteria.ToLower());

            switch (FilterBy)
            {
                case "Initials":
                    {
                        listOfPredicates.Add(initials);
                        break;
                    }
                case "FirstName":
                    {
                        listOfPredicates.Add(firstName);
                        break;
                    }
                case "LastName":
                    {
                        listOfPredicates.Add(lastName);
                        break;
                    }
                case "Email":
                    {
                        listOfPredicates.Add(email);
                        break;
                    }
                case "PhoneNumber":
                    {
                        listOfPredicates.Add(phoneNumber);
                        break;
                    }
                default:
                    {
                        listOfPredicates.Add(initials);
                        listOfPredicates.Add(firstName);
                        listOfPredicates.Add(lastName);
                        listOfPredicates.Add(email);
                        listOfPredicates.Add(phoneNumber);
                        break;
                    }
            }
            return listOfPredicates;
        }
    }    
}
