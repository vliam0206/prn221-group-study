using Application.IServices;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities.Posts;
using RazorPageWebApp.Extensions;

namespace RazorPageWebApp.Pages.Groups.Posts
{
    public class EditPostModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public EditPostModel(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        [BindProperty]
        public Post? Post { get; set; }
        [BindProperty]
        public string? Content { get; set; }
        public async Task<IActionResult> OnGet(Guid groupId, Guid postId)
        {
            var userId = _claimService.GetCurrentUserId;
            if (userId == Guid.Empty) return RedirectToPage("/auth/login", new { Message = "Please Login To Edit Post" });

            var result = await _unitOfWork.GroupRepository.IsUserInGroup(userId, groupId);

            if (result)
            {
                Post = await _unitOfWork.PostRepository.GetPostByIdAsync(postId);
                Content = Post.Content;
            }

            return result ? Page() : RedirectToPage($"/groups/{groupId}");
        }
        public async Task<IActionResult> OnPost(Guid groupId,Guid postId)
        {
            if (ModelState.IsValid)
            {
                Post = await _unitOfWork.PostRepository.GetPostByIdAsync(postId);
                if (Post == null) return Page();
                Post.Content = Content;
                var result = await _unitOfWork.PostRepository.EditPostAsync(Post);
                HttpContext.Session.SetEntity("NewPost", Post);
                if (result == true) return RedirectToPage($"/groups/{groupId}");
            }
            return Page();
        }
    }
}
