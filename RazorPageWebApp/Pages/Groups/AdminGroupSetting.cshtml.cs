using Application.IServices;
using Domain.Entities.Groups;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Groups;

public class AdminGroupSettingModel : PageModel
{
    private IUnitOfWork _unitOfWork;
    private IClaimService _claimService;

    public AdminGroupSettingModel(IUnitOfWork unitOfWork,
                                    IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
    }

    public Group GroupObj { get; set; }
    public async Task<IActionResult> OnGetAsync(string id)
    {        
        var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(Guid.Parse(id));
        if (group == null)
        {
            return NotFound();
        }
        if (_claimService.GetCurrentUserId != group.AccountCreatedID)
        {
            return new RedirectToPageResult("/Error");
        }
        GroupObj = group;
        return Page();
    }
}
