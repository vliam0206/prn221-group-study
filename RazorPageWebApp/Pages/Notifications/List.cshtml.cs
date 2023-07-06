using Application.Commons;
using Application.IServices;
using Domain.Entities;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Notifications
{
    public class ListModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimServices;

        public ListModel(IUnitOfWork unitOfWork, IClaimService claimServices)
        {
            _unitOfWork = unitOfWork;
            _claimServices = claimServices;
        }

        public Pagination<Notification> Notifications { get; set; }
        public async Task OnGet(int pageIndex = 0, int pageSize = 10)
        {
            var userId = _claimServices.GetCurrentUserId;
            Notifications = await _unitOfWork.NotificationRepository.GetAllUnreadNotificationPagination(userId, pageIndex, pageSize);
        }
        public async Task OnGetHeader(int pageIndex = 0, int pageSize = 10)
        {
            ViewData["HideLayout"] = new();
            var userId = _claimServices.GetCurrentUserId;
            Notifications = await _unitOfWork.NotificationRepository.GetAllUnreadNotificationPagination(userId, pageIndex, pageSize);
        }
    }
}
