using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolSecretaries
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<SchoolSecretary, int> _schoolSecRepoAsync;
        private IFilterFunction _filterFunction;
        public List<SchoolSecretary> Secretaries { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        public IndexModel(IRepoAsync<SchoolSecretary, int> schoolSecRepo, IFilterFunction filterFunction)
        {
            _schoolSecRepoAsync = schoolSecRepo;
            _filterFunction = filterFunction;
        }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Secretaries = _filterFunction.FilterFunc(await _schoolSecRepoAsync.GetAllAsync(), FilterBySecretaryPredicate());
            }
            else
            {
                Secretaries = await _schoolSecRepoAsync.GetAllAsync();
            }
        }

        /// <summary>
        /// Creates a list of predicates for filtering SchoolSecretary objects based on the current filter criteria and
        /// selected filter field.
        /// </summary>
        /// <remarks>The returned predicates perform case-insensitive substring matching on the
        /// SchoolSecretary's first name, last name, phone number, email, or associated school name, depending on the
        /// selected filter field. If no specific filter field is set, predicates for all supported fields are included
        /// in the list.</remarks>
        /// <returns>A list of predicates that evaluate whether a SchoolSecretary matches the specified filter criteria. The list
        /// contains one predicate if a specific field is selected, or multiple predicates if no specific field is set.</returns>
        public List<Predicate<SchoolSecretary>> FilterBySecretaryPredicate()
        {
            List<Predicate<SchoolSecretary>> listOfPredicates = new List<Predicate<SchoolSecretary>>();

            Predicate<SchoolSecretary> firstNames = p => p.FirstName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<SchoolSecretary> lastNames = p => p.LastName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<SchoolSecretary> schools = p => p.TheSchool.Name.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<SchoolSecretary> phoneNumbers = p => p.PhoneNumber.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<SchoolSecretary> emails = p => p.Email.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<SchoolSecretary> initials = p => p.Initials.ToLower().Contains(FilterCriteria.ToLower());

            switch (FilterBy)
            {
                case "FirstName":
                    {
                        listOfPredicates.Add(firstNames);
                        break;
                    }
                case "LastName":
                    {
                        listOfPredicates.Add(lastNames);
                        break;
                    }
                case "PhoneNumber":
                    {
                        listOfPredicates.Add(phoneNumbers);
                        break;
                    }
                case "Email":
                    {
                        listOfPredicates.Add(emails);
                        break;
                    }
                case "Initials":
                    {
                        listOfPredicates.Add(initials);
                        break;
                    }
                case "TheSchoolName":
                    {
                        listOfPredicates.Add(schools);
                        break;
                    }
                default:
                    {
                        listOfPredicates.Add(firstNames);
                        listOfPredicates.Add(lastNames);
                        listOfPredicates.Add(phoneNumbers);
                        listOfPredicates.Add(emails);
                        listOfPredicates.Add(schools);
                        break;
                    }
            }
            return listOfPredicates;


        }
    }
}
