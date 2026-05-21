using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Helpers;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photographers
{
    public class IndexModel : PageModel
    {

        IRepoAsync<Photographer, int> _photographerRepoAsync;
        IFilterFunction _filterFunction;

        public List<Photographer> Photographers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }
        public IndexModel(IRepoAsync<Photographer, int> photographerRepoAsync, IFilterFunction filterFunction)
        {
            _photographerRepoAsync = photographerRepoAsync;
            _filterFunction = filterFunction;
        }

        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                var listOfPreds = FilterPhotographersByPredictes();
                Photographers = _filterFunction.FilterFunc(await _photographerRepoAsync.GetAllAsync(), listOfPreds);
            }
            else
            {
                Photographers = await _photographerRepoAsync.GetAllAsync();
            }

            
        }

        public List<Predicate<Photographer>> FilterPhotographersByPredictes()
        {
            List<Predicate<Photographer>> listOfPredicates = new List<Predicate<Photographer>>();

            Predicate<Photographer> firstNames = p => p.FirstName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Photographer> lastNames = p => p.LastName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Photographer> cities = p => p.City.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Photographer> phoneNumbers = p => p.PhoneNumber.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Photographer> emails = p => p.Email.ToLower().Contains(FilterCriteria.ToLower());

            switch (FilterBy)
            {
                case "FirstName":
                    {
                        listOfPredicates.Add(firstNames);
                        break;
                    }
                case "LastName" :
                    {
                        listOfPredicates.Add(lastNames);
                        break;
                    }
                case "PhoneNumber" :
                    {
                        listOfPredicates.Add(phoneNumbers);
                        break;
                    }
                case "Email" :
                    {
                        listOfPredicates.Add(emails);
                        break;
                    }
                //case "Experience" :
                //    {
                //        listOfPredicates.Add(experience);
                //        break;
                    //}
                case "City" :
                    {
                        listOfPredicates.Add(cities);
                        break;
                    }
                default:
                    {
                        listOfPredicates.Add(firstNames);
                        listOfPredicates.Add(lastNames);
                        listOfPredicates.Add(phoneNumbers);
                        listOfPredicates.Add(emails);
                        //listOfPredicates.Add(experience);
                        listOfPredicates.Add(cities);
                        break;
                    }

            }
            return listOfPredicates;

        }

    }
}
