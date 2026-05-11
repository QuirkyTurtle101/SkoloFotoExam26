using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolClasses
{
    public class CreateSchoolClassModel : PageModel
    {
        private IRepoAsync<SchoolClass, int> _schoolClassRepo;

        private IRepoAsync<School, int> _schoolRepo;

        [BindProperty]
        public int SchoolID { get; set; }

        [BindProperty]
        public string ClassName { get; set; }

        public List<School> SchoolList { get; set; }

        public SchoolClass NewSchoolClass { get; set; }

        public CreateSchoolClassModel(IRepoAsync<SchoolClass, int> schoolClassRepo, IRepoAsync<School, int> schoolRepo)
        {
            _schoolClassRepo = schoolClassRepo;
            _schoolRepo = schoolRepo;
        }


        public async Task OnGet()
        {
            SchoolList = await _schoolRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                School school = await _schoolRepo.GetAsync(SchoolID);
                NewSchoolClass = new SchoolClass(ClassName, school);
                await _schoolClassRepo.AddAsync(NewSchoolClass);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");

        }
    }
}
