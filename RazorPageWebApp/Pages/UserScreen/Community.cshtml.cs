using Application.Commons;
using Domain.Entities.Groups;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.UserScreen;

public class CommunityModel : PageModel
{
    private IUnitOfWork _unitOfWork;
    public CommunityModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        Groups = await _unitOfWork.GroupRepository.ToPagination(index, AppConstants.GROUP_PAGE_SIZE);
        UnitOfWork = _unitOfWork;
    }
}
