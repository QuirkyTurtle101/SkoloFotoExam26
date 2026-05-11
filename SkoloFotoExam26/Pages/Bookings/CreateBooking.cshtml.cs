using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        //    private IRepoAsync<Booking, int> _bookingRepo;

        private IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        private IRepoAsync<Teacher, int> _teacherRepo;

        public IRepoAsync<SchoolClass, int> _schoolClassRepo;

        [BindProperty]
        public PhotographingEvent TheEvent { get; set; }

        [BindProperty]
        public DateTime Start { get; set; } = DateTime.Today;
        [BindProperty]
        public DateTime End { get; set; } = DateTime.Today;
        
        [BindProperty]
        public int TeacherID { get; set; }
        [BindProperty]
        public int SchoolClassID { get; set; }

        public List<Teacher> TeacherList { get; set; }

        public List<SchoolClass> SchoolClassList { get; set; }

        public CreateBookingModel(/*IRepoAsync<Booking, int> bookingRepo,*/ IRepoAsync<PhotographingEvent, int> photographingEventRepo,
            IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            //_bookingRepo = bookingRepo;
            _photographingEventRepo = photographingEventRepo;
            _teacherRepo = teacherRepo;
            _schoolClassRepo = schoolClassRepo;
        }

        public async Task OnGet(int id)
        {
            TeacherList = await _teacherRepo.GetAllAsync();
            TheEvent = await _photographingEventRepo.GetAsync(id);
            SchoolClassList = await _schoolClassRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                Teacher teacher = await _teacherRepo.GetAsync(TeacherID);
                SchoolClass schoolClass = await _schoolClassRepo.GetAsync(SchoolClassID);
                Booking booking = new Booking(Start, End, TheEvent, teacher, schoolClass);
            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("ChooseEvents");
        }
    }
}
