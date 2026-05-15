using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.SchoolClasses
{
    public class EditSchoolClassModel : PageModel
    {
        private IRepoAsync<SchoolClass, int> _schoolClassRepo;

        private IRepoAsync<School, int> _schoolRepo;

        [BindProperty]
        public SchoolClass EditSchoolClass { get; set; }

        [BindProperty]
        public int SchoolID { get; set; }
        [BindProperty]
        public string ClassName { get; set; }

        [BindProperty]
        public int TheSchoolClassID { get; set; }

        public List<School> SchoolList { get; set; }



        public EditSchoolClassModel(IRepoAsync<SchoolClass, int> schoolClassRepo, IRepoAsync<School, int> schoolRepo)
        {
            _schoolClassRepo = schoolClassRepo;
            _schoolRepo = schoolRepo;

        }
        public async Task OnGet(int SchoolClassID)
        {
            SchoolList = await _schoolRepo.GetAllAsync();
            EditSchoolClass = await _schoolClassRepo.GetAsync(SchoolClassID);
            SchoolID = EditSchoolClass.School.SchoolID;
            ClassName = EditSchoolClass.ClassName;
            TheSchoolClassID = EditSchoolClass.SchoolClassID;
        }

        public async Task<IActionResult> OnPostUpdate(int SchoolClassID)
        {
            try
            {
                SchoolList = await _schoolRepo.GetAllAsync();

                SchoolClass edited = new SchoolClass(SchoolClassID, ClassName,
                    await _schoolRepo.GetAsync(SchoolID));
                await _schoolClassRepo.UpdateAsync(edited);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ViewData["Title"] = ex.Message;
                return Page();
            }
        }
    }
}
