using Application.Commons;
using Application.IServices;
using Domain.Entities.Groups;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWebApp.Services;

namespace RazorPageWebApp.Pages.UserScreen;

public class MyGroupModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;

    public MyGroupModel(IUnitOfWork unitOfWork,
                        IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
    }
    public Pagination<Group> Groups { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }
    public async Task OnGetAsync(int? pageIndex)
    {
        var index = 0;
        if (pageIndex != null)
        {
            index = pageIndex.Value;
        }
        var currentId = _claimService.GetCurrentUserId;
        Groups = await _unitOfWork.GroupRepository.GetGroupsToPaginAsync(currentId, index, AppConstants.GROUP_PAGE_SIZE);
        UnitOfWork = _unitOfWork;
    }
}
