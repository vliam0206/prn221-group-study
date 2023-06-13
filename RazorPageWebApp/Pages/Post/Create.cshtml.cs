using Infrastructure.Repositories.Posts;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorPageWebApp.Pages.Post
{
    public class CreatePostModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostModel(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [BindProperty]
        [Required(ErrorMessage ="Post Content shouldn't be empty")]
        public string? TextDs { get; set; } = "<h1>Welcome to TinyMce</h1>";
        public void OnGet(Guid? groupId)
        {
           

        }
        public async Task<IActionResult> OnPostAsync(Guid? groupId)
        {
            if (ModelState.IsValid)
            {
                var result =  await _unitOfWork.PostRepository.AddPostAsync(groupId, TextDs);

                if (result == true) return Page();
            }
            return Redirect("/Error");
        }
    }
}
