using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkiaSharp;


namespace SkoloFotoExam26.Pages.Photos
{
    public class CreatePhotoModel : PageModel
    {
        IRepoAsync<Photo, int> _photos;

        //IRepoAsync<Booking, int> _bookings;

        private IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public string FileName { get; set; }

        public string FilePath { get; set; }

        [BindProperty]
        public double Price { get; set; }

        [BindProperty]
        public DateTime TheDate { get; set; } = DateTime.Today;

        public int Width { get; set; }

        public int Height { get; set; }

        [BindProperty]
        public PhotoType PhotoType { get; set; }

        //[BindProperty]
        //public int BookingID { get; set; }

        
        //public List<Booking> BookingList { get; set; }

        public CreatePhotoModel(IRepoAsync<Photo, int> photos, IWebHostEnvironment webHost)
        {
            _photos = photos;
            webHostEnvironment = webHost;
        }
        public async Task OnGetAsync()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Photo != null)
            {
                FileName = ProcessUploadedFile();

                FilePath = Path.Combine(webHostEnvironment.WebRootPath, "assets\\images\\uploaded", FileName);

                //would you believe this was the hardest part of the entire project
                //i'm going to beat microsoft with hammers until System.Drawing.Common becomes cross platform again
                var imageHeader = SKBitmap.DecodeBounds(Photo.OpenReadStream());
                Width = imageHeader.Width;
                Height = imageHeader.Height;

                try
                {
                    //Booking theBooking = await _bookings.GetAsync(BookingID);
                    Photo photo = new Photo(FileName, FilePath, Price, TheDate, Height, Width, PhotoType);
                    await _photos.AddAsync(photo);

                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                }
            }
            return RedirectToPage("Index");
        }




        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets\\images\\uploaded");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
