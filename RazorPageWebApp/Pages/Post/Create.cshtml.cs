using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Post
{
    public class CreatePostModel : PageModel
    {
        [BindProperty]
        public string? TextDs { get; set; } = "<h1>Welcome to TinyMce</h1>";
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
