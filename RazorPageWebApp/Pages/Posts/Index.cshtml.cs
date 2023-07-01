using Application.IServices;
using Domain.Entities.Posts;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace RazorPageWebApp.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly IClaimService _claimService;
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public Post? Post { get; private set; }
        [BindProperty]
        public string Content { get; set; } 
        [BindProperty]
        public Comment? Comment { get; set; }
        public async Task<IActionResult> OnGet(Guid groupId, Guid postId)
        {
            var userId = _claimService.GetCurrentUserId;
            if (userId == Guid.Empty) return RedirectToPage("/auth/login", new { Message = "Please Login To View Post" });

            var result = await _unitOfWork.GroupRepository.IsUserInGroup(userId, groupId);

            if (result)
            {
                Post = await _unitOfWork.PostRepository.GetPostByIdAsync(postId);
                Content = Post.Content;
            }

            return result ? Page() : RedirectToPage($"/groups/{groupId}");
        }

        //Post method only
        [ActionName("Comment")]
        public async Task<IActionResult> OnPostComment(Guid groupId, Guid postId)
        {
            var userId = _claimService.GetCurrentUserId;
            if (userId == Guid.Empty) return RedirectToPage("/auth/login", new { Message = "Please Login To View Post" });
            var result = await _unitOfWork.GroupRepository.IsUserInGroup(userId, groupId);

            if (result)
            {
                if (Comment == null) return NotFound();
                if (ModelState.IsValid)
                {
                    Comment.PostId = postId;
                    Comment.AccountCreatedID = _claimService.GetCurrentUserId;
                    result = await _unitOfWork.CommentRepository.AddCommentAsync(Comment);
                    if (result)
                        return new JsonResult(Comment);
                }
                
            }
           
            return BadRequest();
        }
    }
}
