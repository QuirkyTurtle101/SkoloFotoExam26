using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photos
{
    public class IndexModel : PageModel
    {

        IRepoAsync<Photo, int> _photos;

        public List<Photo> PhotoList { get; set; }

        public IndexModel(IRepoAsync<Photo, int> photos)
        {
            _photos = photos;
        }
        public async Task OnGet()
        {
            PhotoList = await _photos.GetAllAsync();
        }
    }
}
