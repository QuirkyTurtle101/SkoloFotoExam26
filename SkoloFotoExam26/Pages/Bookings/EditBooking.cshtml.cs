using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class EditBookingModel : PageModel
    {
        private IRepoAsync<Booking, int> _bookingRepo;

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
            _bookingRepo = bookingRepo;
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
                TeacherList = await _teacherRepo.GetAllAsync();
                EditBooking = await _bookingRepo.GetAsync(BookingID);
                SchoolClassList = await _schoolClassRepo.GetAllAsync();

                Booking edited = new Booking(Start, End, await _photographingEventRepo.GetAsync(TheEventID),
                    await _schoolClassRepo.GetAsync(SelectedSchoolClassID), 
                    await _teacherRepo.GetAsync(TeacherID), BookingID);
                await _bookingRepo.UpdateAsync(edited);
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
