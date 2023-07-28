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
        Notification Notification { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid postId, string type)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(postId);
            var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);

            if (post == null) return NotFound("Post Not Found");
            if (user == null) return NotFound("User Not Found");
            if (type.Equals("like", StringComparison.OrdinalIgnoreCase))
            {
                //bool hadLiked = await _unitOfWork.AccountRepository.IsUserLiked(postId, user.Id);
                //if (hadLiked)
                //{
                //    // go back 
                //    return new JsonResult("No need to create notification");
                //}
                var hadNotify =  _unitOfWork.NotificationRepository.HadNotify(post.AccountCreatedID.Value, user.Id, Domain.Enums.NotiTypeEnum.Like);
                if(!hadNotify)
                await CreateLikeNotification(post, user);
                else
                return new JsonResult("No need to create notification");

            }
            if (type.Equals("comment", StringComparison.OrdinalIgnoreCase))
            {
                await CreateCommentNotification(post, user);
            }
            if (type.Equals("reply", StringComparison.OrdinalIgnoreCase))
            {
                await CreateCommentReplyNotification(post, user);
            }
            else return BadRequest();
            //await _notifyHub.SendNotifyOther(Notification);
            return new JsonResult(Notification);
        }

        private async Task CreateCommentReplyNotification(Post post, Account user)
        {
            Notification.FromAccountId = _claimService.GetCurrentUserId;
            Notification.Type = Domain.Enums.NotiTypeEnum.Comment;
            Notification.Status = Domain.Enums.NotiStatusEnum.Unread;
            Notification.AccountRecievedId = post.AccountCreatedID.Value;
            Notification.Content = $"User <b>{user.Username}</b> have Replied your Comment <b>{post.Topic}</b>";
            await _unitOfWork.NotificationRepository.AddAsync(Notification);
        }

        private async Task CreateCommentNotification(Post post, Account user)
        {
            Notification.FromAccountId = _claimService.GetCurrentUserId;
            Notification.Type = Domain.Enums.NotiTypeEnum.Comment;
            Notification.Status = Domain.Enums.NotiStatusEnum.Unread;
            Notification.AccountRecievedId = post.AccountCreatedID.Value;
            Notification.Content = $"User <b>{user.Username}</b> have Commented your Post <b>{post.Topic}</b>";
            await _unitOfWork.NotificationRepository.AddAsync(Notification);
        }

        private async Task CreateLikeNotification(Post? post, Account? user)
        {
            Notification.FromAccountId = _claimService.GetCurrentUserId;
            Notification.Type = Domain.Enums.NotiTypeEnum.Like;
            Notification.Status = Domain.Enums.NotiStatusEnum.Unread;
            Notification.AccountRecievedId = post.AccountCreatedID.Value;
            Notification.Content = $"User <b>{user.Username}</b> have liked your Post <b>{post.Topic}</b>";
            await _unitOfWork.NotificationRepository.AddAsync(Notification);
        }


    }
}
