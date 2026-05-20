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
        public int SelectedSchoolClassID { get; set; }

        public List<Teacher> TeacherList { get; set; }

        public List<SchoolClass> SchoolClassList { get; set; }

        public List<Booking> Bookings { get; set; }

        public CreateBookingModel(IRepoAsync<PhotographingEvent, int> photographingEventRepo, IRepoAsync<Booking, int> bookingRepo,
            IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            _bookingRepo = bookingRepo;
            _photographingEventRepo = photographingEventRepo;
            _teacherRepo = teacherRepo;
            _schoolClassRepo = schoolClassRepo;
        }

        public async Task OnGet(int id)
        {
            TeacherList = await _teacherRepo.GetAllAsync();
            TheEvent = await _photographingEventRepo.GetAsync(id);
            SchoolClassList = await _schoolClassRepo.GetAllAsync();
            Bookings = await _bookingRepo.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                TeacherList = await _teacherRepo.GetAllAsync();
                SchoolClassList = await _schoolClassRepo.GetAllAsync();
                Teacher teacher = await _teacherRepo.GetAsync(TeacherID);
                TheEvent = await _photographingEventRepo.GetAsync(id);
                SchoolClass schoolClass = await _schoolClassRepo.GetAsync(SelectedSchoolClassID);
                Bookings = await _bookingRepo.GetAllAsync();
                Booking booking = new Booking(Start, End, TheEvent, teacher, schoolClass);
                if (booking.Start < TheEvent.Start || booking.End > TheEvent.End)
                {
                    ViewData["ErrorMessage"] = "Bookingen skal vaere i eventets tidsramme";
                    return Page();
                }

                foreach (Booking b in Bookings)
                {         
                    if (booking.Start < b.End && booking.End > b.Start)
                    {
                        if (b.ThePhotographingEvent.PhotographingEventID == booking.ThePhotographingEvent.PhotographingEventID)
                        {
                            ViewData["ErrorMessage"] = "Tiden er booket";
                            return Page();
                        }
                    }

                }
                await _bookingRepo.AddAsync(booking);

            }
            catch (Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
