using Application.Commons;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
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
    public async Task OnGetAsync(Guid groupId)
    {
        Posts = await _unitOfWork.PostRepository.GetAllPostFromGroupAsync(groupId, 1, 10);
        GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
    }
}
