using Application.IServices;
using Infrastructure.IRepositories;
using RazorPageWebApp;
using RazorPageWebApp.Extensions;

public class LoadNotificationMiddleware : IMiddleware
{
    private readonly INotificationRepository _repository;
    private readonly IClaimService _claimService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoadNotificationMiddleware(INotificationRepository repository, IClaimService claimService, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _claimService = claimService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Guid currentUserId = _claimService.GetCurrentUserId;
        if (currentUserId == Guid.Empty)
        {
            await next(context);
            return;
        }

        if (currentUserId != Guid.Empty)
        {
            _httpContextAccessor?.HttpContext?.Session.SetEntity(AppConstants.GetNotifySession(currentUserId)
                , await _repository.GetAllUnreadNotificationPagination(currentUserId, 0, 10));
        }
        await next(context);

    }

   
}