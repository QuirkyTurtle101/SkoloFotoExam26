using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

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

        [BindProperty]
        public string FilePath { get; set; }

        [BindProperty]
        public double Price { get; set; }

        [BindProperty]
        public DateTime TheDate { get; set; } = DateTime.Today;

        [BindProperty]
        public int Height { get; set; }

        [BindProperty]
        public int Width { get; set; }

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
        public async Task OnPostAsync()
        {
            if (Photo != null)
            {
                if (FileName != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/Images/PhotografImages", FileName);
                    System.IO.File.Delete(filePath);
                }

                FileName = ProcessUploadedFile();
            }
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




        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/PhotografImages");
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
