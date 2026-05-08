using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IRepoAsync<Booking, int> _bookingRepo;

        private IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        private IRepoAsync<Teacher, int> _teacherRepo;

        public IRepoAsync<SchoolClass, int> _schoolClass;

        [BindProperty]
        public PhotographingEvent TheEvent { get; set; }

        [BindProperty]
        public DateTime Start { get; set; }
        [BindProperty]
        public DateTime End { get; set; }
        
        [BindProperty]
        public int TeacherID { get; set; }
        [BindProperty]
        public int SchoolClassID { get; set; }

        public List<Teacher> TeacherList { get; set; }

        public CreateBookingModel(IRepoAsync<Booking, int> bookingRepo, IRepoAsync<PhotographingEvent, int> photographingEventRepo,
            IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<SchoolClass, int> schoolClass)
        {
            _bookingRepo = bookingRepo;
            _photographingEventRepo = photographingEventRepo;
            _teacherRepo = teacherRepo;
            _schoolClass = schoolClass;
        }

        public async Task OnGet(int id)
        {
            TeacherList = await _teacherRepo.GetAllAsync();
            //TheEvent = await _photographingEvent.GetAsync(id);
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    try
        //    {

        //        Photographer thePhotographer = await _photographerRepo.GetAsync(PhotographerID);
        //        //SchoolClass schoolClass = await _schoolClass.GetAsync()
        //        Booking booking = new Booking(Start, End, TheEvent, thePhotographer, theSchoolClass);
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewData["ErrorMessage"] = ex.Message;
        //        return Page();
        //    }
        //}
    }
}
