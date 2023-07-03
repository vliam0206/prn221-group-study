using Application.IServices;
using Domain.Entities.Posts;
using Domain.Enums;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPageWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClaimService _claimService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;

        public string? Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IClaimService claimService,
                            IHttpContextAccessor httpContext,
                            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _claimService = claimService;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetCurrentId()
        {
            Message = $"Current UserId: {_claimService.GetCurrentUserId}";
            return Page();
        }
        public IActionResult OnGetCurrentUsername()
        {
            Message = $"Current UserName: {_claimService.GetCurrentUserName}";
            return Page();
        }
        public IActionResult OnGetLogOut()
        {
            _httpContext.HttpContext!.Session.Clear();
            Message = "Logged out successfully!";
            return Page();
        }
        public async Task<IActionResult> OnGetUpdateStatus(Guid id, string status, Guid groupId)
        {
            var GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            Post post = await _unitOfWork.PostRepository.GetPostByIdAsync(id);
            if (post == null) return Redirect($"/Groups/AllPosts?groupId={GroupObj.Id}");
            post.Status = (PostStatusEnum)Enum.Parse(typeof(PostStatusEnum), status);
            await _unitOfWork.PostRepository.EditPostAsync(post);
            return Redirect($"/Groups/AllPosts?groupId={GroupObj.Id}");
        }
    }
}