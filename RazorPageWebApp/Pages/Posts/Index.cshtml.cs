using Application.IServices;
using Domain.Entities;
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

        public Account? CurUser { get; private set; }
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
                Post.Likes = Post.Likes.Where(x => x.IsDeleted == false && x.Status == Domain.Enums.LikeStatusEnum.Like ).ToList();
                Post.Comments = Post.Comments.Where(x => x.CommentRepliedId == null).Select(x =>
                {
                    x.ReplyComments = x.ReplyComments.Where(x => x.IsDeleted == false).ToList();
                    return x;
                }).ToList();
                
                Content = Post.Content;
            }
            CurUser = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId); 
            return result ? Page() : RedirectToPage($"/groups/Details", new { id = groupId });
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
                    Comment.AccountCreatedID = _claimService.GetCurrentUserId;
                    result = await _unitOfWork.CommentRepository.AddCommentAsync(Comment);
                    if (result)
                    {
                        if (Comment.CommentRepliedId == null) return RedirectToPage("/Comments/Index", new { id = Comment.Id, comment = Comment });
                        else { return RedirectToPage("/Comments/Reply", new { id = Comment.Id, commentId = Comment.Id }); }
                    }
                }

            }

            return BadRequest();
        }
        [ActionName("Delete")]
        public async Task<IActionResult> OnPostDelete(Guid groupId, Guid postId)
        {
            var userId = _claimService.GetCurrentUserId;
            if (userId == Guid.Empty) return RedirectToPage("/auth/login", new { Message = "Please Login To View Post" });
            var result = await _unitOfWork.GroupRepository.IsUserInGroup(userId, groupId);

            if (result)
            {
                
                if (ModelState.ErrorCount == 1)
                {
                    await _unitOfWork.PostRepository.RemoveAsyncId(postId);
                    return RedirectToPage("/Groups/Details", new
                    {
                        id= groupId
                    });
                }

            }
            return RedirectToAction("OnGet", new { groupId, postId });
        }
       
    }
}
