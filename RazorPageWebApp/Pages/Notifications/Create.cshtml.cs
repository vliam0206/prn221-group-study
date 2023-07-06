using Application.IServices;
using Domain.Entities;
using Domain.Entities.Posts;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWebApp.Extensions;

namespace RazorPageWebApp.Pages.Notifications
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;

        public CreateModel(IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }
        Notification Notification { get; set; }

        public async Task<IActionResult> OnPostAsync(Guid postId, string type)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(postId);
            if (post == null) return BadRequest();

            if (type.Equals("like", StringComparison.OrdinalIgnoreCase))
            {
                var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);
                if (user == null) return BadRequest();
                bool hadLiked = await _unitOfWork.AccountRepository.IsUserLiked(postId, user.Id);
                if (hadLiked)
                {
                    await _unitOfWork.LikeRepository.ToggleLikeAsync(postId, user.Id);
                    // go back 
                    return BadRequest("No need to create notification");
                }


                await AddLike(post, user);

                Notification.FromAccountId = _claimService.GetCurrentUserId;
                Notification.Type = Domain.Enums.NotiTypeEnum.Like;
                Notification.Status = Domain.Enums.NotiStatusEnum.Unread;
                Notification.AccountRecievedId = post.AccountCreatedID.Value;
                Notification.Content = $"User <b>{user.Username}</b> have liked your Post <b>{post.Topic}</b>";
                await _unitOfWork.NotificationRepository.AddAsync(Notification);
            }
            else if (type.Equals("comment", StringComparison.OrdinalIgnoreCase))
            {

            }
            else return BadRequest();
            return new JsonResult(Notification);
        }

        private async Task AddLike(Post? post, Account? user)
        {
            // add like
            var like = new Like()
            {
                AccountCreatedID = user.Id,
                PostId = post.Id
            };
            await _unitOfWork.LikeRepository.AddAsync(like);
        }

    }
}
