using Application.IServices;
using Domain.Entities;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Notifications
{
    public class ViewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimServices;

        public ViewModel(IUnitOfWork unitOfWork, IClaimService claimServices)
        {
            _unitOfWork = unitOfWork;
            _claimServices = claimServices;
        }
        public async Task<IActionResult> OnGetAsync(Guid id, string path)
        {
            Notification? notification = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (notification == null) return NotFound();
            notification.Status = Domain.Enums.NotiStatusEnum.Read;
            await _unitOfWork.NotificationRepository.UpdateAsync(notification);

            //unfortunately i can't add this to database
            //if(notification.Type== Domain.Enums.NotiTypeEnum.Like || notification.Type == Domain.Enums.NotiTypeEnum.Comment)
            //{
            //    return RedirectToPage("/Posts/Index", new { });
            //}

            return Redirect(path);
        }
    }
}
