using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Entities.Groups;
using Infrastructure.IRepositories.Groups;
using Infrastructure.UnitOfWorks;
using Domain.Entities.Posts;
using Application.Commons;
using Domain.Entities;
using Application.IServices;

namespace RazorPageWebApp.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IClaimService _claimService;
        private IUnitOfWork _unitOfWork;

        public DetailsModel(IHttpContextAccessor httpContext, IUnitOfWork unitOfWork, IClaimService claimService)
        {
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }

        public Group Group { get; set; } = default!;
        public AccountInGroup? AccountInGroup { get; set; } = default!;
        public Pagination<Post> PostsInGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            else
            {
                Group = group;
                AccountInGroup = await _unitOfWork.AccountInGroupRepository.GetAccountInGroupAsync(_claimService.GetCurrentUserId, id);
                PostsInGroup = await _unitOfWork.PostRepository.GetAllPostFromGroupAsync(id, 1, 10);
            }
            return Page();
        }
    }
}
