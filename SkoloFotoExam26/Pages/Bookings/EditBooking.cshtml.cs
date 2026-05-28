using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class EditBookingModel : PageModel
    {
        private BookingRepoAsync _bookingRepo;

        private IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        private IRepoAsync<Teacher, int> _teacherRepo;

        public IRepoAsync<SchoolClass, int> _schoolClassRepo;

        [BindProperty]
        public Booking EditBooking { get; set; }

        [BindProperty]
        public DateTime Start { get; set; } = DateTime.Today;
        [BindProperty]
        public DateTime End { get; set; } = DateTime.Today;

        [BindProperty]
        public int TheEventID { get; set; }

        [BindProperty]
        public int TeacherID { get; set; }
        [BindProperty]
        public int SelectedSchoolClassID { get; set; }

        public List<Teacher> TeacherList { get; set; }

        public List<SchoolClass> SchoolClassList { get; set; }

        public List<Booking> Bookings { get; set; }

        public EditBookingModel(IRepoAsync<PhotographingEvent, int> photographingEventRepo, IRepoAsync<Booking, int> bookingRepo,
            IRepoAsync<Teacher, int> teacherRepo, IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            _bookingRepo = (BookingRepoAsync)bookingRepo;
            _photographingEventRepo = photographingEventRepo;
            _teacherRepo = teacherRepo;
            _schoolClassRepo = schoolClassRepo;
        }

        public async Task OnGet(int BookingID)
        {
            TeacherList = await _teacherRepo.GetAllAsync();
            EditBooking = await _bookingRepo.GetAsync(BookingID);
            SchoolClassList = await _schoolClassRepo.GetAllAsync();
            Start = EditBooking.Start;
            End = EditBooking.End;
            TeacherID = EditBooking.TheTeacher.ID;
            SelectedSchoolClassID = EditBooking.TheSchoolClass.SchoolClassID;
            TheEventID = EditBooking.ThePhotographingEvent.PhotographingEventID;
        }

        public async Task<IActionResult> OnPostUpdate(int BookingID)
        {
            try
            {
                Bookings = await _bookingRepo.GetAllAsync();
                TeacherList = await _teacherRepo.GetAllAsync();
                EditBooking = await _bookingRepo.GetAsync(BookingID);
                SchoolClassList = await _schoolClassRepo.GetAllAsync();
                PhotographingEvent TheEvent = await _photographingEventRepo.GetAsync(EditBooking.ThePhotographingEvent.PhotographingEventID);
                Booking edited = new Booking(Start, End, TheEvent,
                    await _schoolClassRepo.GetAsync(SelectedSchoolClassID), 
                    await _teacherRepo.GetAsync(TeacherID), BookingID);
                await _bookingRepo.BookingCheckAsync(edited, TheEvent);
                await _bookingRepo.UpdateAsync(edited);

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }

        }



    }
}
