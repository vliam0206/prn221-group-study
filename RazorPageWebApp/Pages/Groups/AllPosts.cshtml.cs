using Application.Commons;
using Domain.Entities;
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
    public async Task<IActionResult> OnGetAsync(Guid groupId, int? pageIndex, string? searchValue)
    {
        var index = 0;
        if (pageIndex != null)
        {
            index = pageIndex.Value;
        }
        if (searchValue != null) {
            Posts = await _unitOfWork.PostRepository.GetAllPostFromGroupSearchAsync(groupId, searchValue, index, AppConstants.POST_PAGE_SIZE);
        } else {
            Posts = await _unitOfWork.PostRepository.GetAllPostFromGroupAsync(groupId, index, AppConstants.POST_PAGE_SIZE);
        }
        GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
        if (GroupObj == null)
        {
            return NotFound();
        }
        return Page();
    }
    public async Task<IActionResult> OnGetDeleteAsync(Guid groupId, Guid postId) {
        GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
        if (GroupObj == null) {
            return NotFound();
        }
        Posts = await _unitOfWork.PostRepository.GetAllPostFromGroupAsync(groupId, 0, AppConstants.POST_PAGE_SIZE);

        await _unitOfWork.PostRepository.RemoveAsyncId(postId);

        return Page();
    }
}
