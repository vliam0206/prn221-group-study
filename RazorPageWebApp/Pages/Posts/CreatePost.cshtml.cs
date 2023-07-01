using Application.IServices;
using Infrastructure.Repositories.Posts;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        [BindProperty]
        [Required(ErrorMessage = "Post Content shouldn't be empty")]
        public string? Content { get; set; } = string.Empty;
        //[Authorize]
        public async Task<IActionResult> OnGetAsync(Guid groupId)
        {
            if (Debugger.IsAttached)
            {

            }
            var userId = _claimService.GetCurrentUserId;
            if (userId == Guid.Empty) return RedirectToPage("/auth/login",new { Message ="Please Login To Create Post"});

            var result = await _unitOfWork.GroupRepository.IsUserInGroup(userId, groupId);
            return result ? Page() : RedirectToPage("/index");
        }
        public async Task<IActionResult> OnPostAsync(Guid? groupId)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.PostRepository.AddPostAsync(groupId, Content);

                if (result == true) return Redirect($"/groups/{groupId}");
            }
            return Page();
        }
    }
}
