using Application.IServices;
using Domain.Entities.Groups;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.UserScreen;

public class HomePageModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;

    public HomePageModel(IUnitOfWork unitOfWork,
                        IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
    }
    public List<Group> CommunityGroup { get; set; }
    public List<Group> OwnGroup { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }
    public async Task OnGetAsync()
    {
        UnitOfWork = _unitOfWork;
        var currentId = _claimService.GetCurrentUserId;
        CommunityGroup = await _unitOfWork.GroupRepository.GetTopGroupsAsync(AppConstants.GROUP_PAGE_SIZE);
        OwnGroup = await _unitOfWork.GroupRepository.GetTopGroupsAsync(currentId, AppConstants.TOP_GROUP_NUM);
    }
}
