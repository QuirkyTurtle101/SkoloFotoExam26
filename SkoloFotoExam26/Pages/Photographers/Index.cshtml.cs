using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Pages.Photographers
{
    public class IndexModel : PageModel
    {

        IRepoAsync<Photographer, int> _photographerRepoAsync;

        public List<Photographer> Photographers { get; set; }

        public IndexModel(IRepoAsync<Photographer, int> photographerRepoAsync)
        {
            _photographerRepoAsync = photographerRepoAsync;
        }

        public async Task OnGet()
        {
            Photographers = await _photographerRepoAsync.GetAllAsync();
        }

    }
}
