using Application.IServices;
using Domain.Entities.Posts;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Comments
{
    public class ReplyModel : PageModel
    {
        private readonly IClaimService _claimService;
        private readonly IUnitOfWork _unitOfWork;
        public ReplyModel(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }
        [BindProperty]
        public Comment Comment { get; set; }
        [BindProperty]
        public Guid CommentId { get; set; }
        public async Task<IActionResult> OnGet(Guid id, Guid commentId)
        {
            CommentId = commentId;
            Comment = await _unitOfWork.CommentRepository.GetByIdAsync(id, x => x.AccountCreated, x => x.AccountReplied, x => x.ReplyComments, x => x.CommentReplied);
            return Page();
        }
    }
}
