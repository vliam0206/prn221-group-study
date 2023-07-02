using Domain.Entities.Groups;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Groups;

public class AdminGroupSettingModel : PageModel
{
    private IUnitOfWork _unitOfWork;

    public AdminGroupSettingModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Group GroupObj { get; set; }
    public async Task OnGetAsync(string id)
    {
        var group = await _unitOfWork.GroupRepository.GetByIdAsync(Guid.Parse(id));
        if (group == null)
        {
            return;
        }
        GroupObj = group;
    }
}
