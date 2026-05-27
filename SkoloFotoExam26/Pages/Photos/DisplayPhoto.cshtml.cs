using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photos
{
    public class DisplayPhotoModel : PageModel
    {
        private IWebHostEnvironment webHostEnvironment;
        private IRepoAsync<Photo, int> _photoRepo;
        public Photo PhotoToShow { get; set; }
        public string ModifiedPath { get; set; }

        public DisplayPhotoModel(IRepoAsync<Photo, int> photoRepo, IWebHostEnvironment webHost)
        {
            _photoRepo = photoRepo;
            webHostEnvironment = webHost;
        }

        public async Task OnGetAsync(int PhotoID)
        {
            PhotoToShow = await _photoRepo.GetAsync(PhotoID);
            ModifiedPath = PhotoToShow.FilePath.Replace(webHostEnvironment.WebRootPath, String.Empty);
        }
    }
}
