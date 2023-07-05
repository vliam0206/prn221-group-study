using Application.Commons;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
using Domain.Enums;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Groups;

public class AllPostsModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public AllPostsModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Pagination<Post> Posts { get; set; }
    public Group GroupObj { get; set; }
    public async Task<IActionResult> OnGetAsync(Guid groupId, int? pageIndex)
    {
        var index = 0;
        if (pageIndex != null)
        {
            index = pageIndex.Value;
        }
        Posts = await _unitOfWork.PostRepository.GetAllPostFromGroupAsync(groupId, index, AppConstants.POST_PAGE_SIZE);
        GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
        if (GroupObj == null)
        {
            return NotFound();
        }
        return Page();
    }
    
}
