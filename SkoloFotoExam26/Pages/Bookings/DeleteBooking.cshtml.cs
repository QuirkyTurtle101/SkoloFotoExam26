using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Bookings
{
    public class DeleteBookingModel : PageModel
    {


            private IRepoAsync<Booking, int> _bookings;
            [BindProperty]

            public Booking BookingDelete { get; set; }

            public DeleteBookingModel(IRepoAsync<Booking, int> bookings)
            {
                _bookings = bookings;

            }
            
            public async Task OnGetAsync(int BookingID)
            {
                BookingDelete = await _bookings.GetAsync(BookingID);
            }

            public async Task<IActionResult> OnPostDeleteAsync(int BookingID)
            {
                try
                {
                    await _bookings.DeleteAsync(BookingID);
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
