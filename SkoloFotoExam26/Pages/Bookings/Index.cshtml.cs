using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private IRepoAsync<Booking, int> _bookingRepo;

        public List<Booking> BookingList { get; set; }

        public IndexModel(IRepoAsync<Booking, int> bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }
        public async Task OnGetAsync()
        {
            BookingList = await _bookingRepo.GetAllAsync();
        }
    }
}
