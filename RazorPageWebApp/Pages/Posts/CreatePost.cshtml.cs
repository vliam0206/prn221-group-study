using Application.IServices;
using Domain.Entities;
using Domain.Entities.Posts;
using Infrastructure.Repositories.Posts;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RazorPageWebApp.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RazorPageWebApp.Pages.Groups
{
    public class CreatePostModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public CreatePostModel(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public Account? User { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Post Content shouldn't be empty")]
        public string? Content { get; set; } = string.Empty;
        [BindProperty]
        [Required(ErrorMessage = "Topic shouldn't be empty")]
        public string? Topic { get; set; } = string.Empty;
        //[Authorize]
        public async Task<IActionResult> OnGetAsync(Guid groupId)
        {
            var userId = _claimService.GetCurrentUserId;
            if (userId == Guid.Empty) return RedirectToPage("/auth/login", new { Message = "Please Login To Create Post" });
            var result = await _unitOfWork.GroupRepository.IsUserInGroup(userId, groupId);
            User = await _unitOfWork.AccountRepository.GetByIdAsync(userId);
            return result ? Page() : RedirectToPage("/index");
        }
        public async Task<IActionResult> OnPostAsync(Guid? groupId)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    GroupId = groupId.Value,
                    Topic = Topic,
                    Content = Content,
                    AccountCreatedID = _claimService.GetCurrentUserId
                };

                await _unitOfWork.PostRepository.AddAsync(post);
                await _unitOfWork.PostRepository.SaveChangesAsync();
                HttpContext.Session.SetEntity("NewPost", post);
                return RedirectToPage($"/groups/details", new
                {
                    id = groupId
                });
            }
            User = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);

            return Page();
        }
    }
}
