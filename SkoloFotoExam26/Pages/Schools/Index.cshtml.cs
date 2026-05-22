using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Schools
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<School, int> _schoolRepo;
        private IFilterFunction _filterFunction;

        public List<School> Schools { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }
        public IndexModel(IRepoAsync<School, int> schoolRepoAsync, IFilterFunction filterFunction)
        {
            _schoolRepo = schoolRepoAsync;
            _filterFunction = filterFunction;
        }
        public async Task OnGet()
        {
            if(!string.IsNullOrEmpty(FilterCriteria))
            {
                Schools = _filterFunction.FilterFunc(await _schoolRepo.GetAllAsync(), FilterBySchoolPredicates());                
            }
            else
            {
                Schools = await _schoolRepo.GetAllAsync();
            }
        }

        public List<Predicate<School>> FilterBySchoolPredicates()
        {
            List<Predicate<School>> predicatesList = new List<Predicate<School>>();
            Predicate<School> names = s => s.Name.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<School> cities = s => s.City.ToLower().Contains(FilterCriteria.ToLower());

            switch (FilterBy)
            {
                case "Name":
                    {
                        predicatesList.Add(names);
                        break;
                    }
                case "City":
                    {
                        predicatesList.Add(cities);
                        break;
                    }
                default:
                    {
                        predicatesList.Add(names);
                        predicatesList.Add(cities);
                        break;
                    }
            }
            return predicatesList;
        }

    }
}
