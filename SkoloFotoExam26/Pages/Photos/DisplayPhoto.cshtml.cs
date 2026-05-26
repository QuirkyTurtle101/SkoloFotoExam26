using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photos
{
    public class DisplayPhotoModel : PageModel
    {
        private IRepoAsync<Photo, int> _photoRepo;
        public Photo PhotoToShow { get; set; }

        public DisplayPhotoModel(IRepoAsync<Photo, int> photoRepo)
        {
            _photoRepo = photoRepo;
        }

        public async Task OnGetAsync(int PhotoID)
        {
            PhotoToShow = await _photoRepo.GetAsync(PhotoID);
        }
    }
}
