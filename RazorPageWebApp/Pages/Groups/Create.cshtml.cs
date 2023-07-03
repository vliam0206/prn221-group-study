using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess;
using Domain.Entities.Groups;
using Infrastructure.IRepositories.Groups;
using Infrastructure.UnitOfWorks;
using Domain.Enums;
using Application.IServices;

namespace RazorPageWebApp.Pages.Groups {
    public class CreateModel : PageModel {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IClaimService _claimService;
        private IUnitOfWork _unitOfWork;

        public CreateModel(IHttpContextAccessor httpContext, IUnitOfWork unitOfWork, IClaimService claimService) {
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }
        public IActionResult OnGet() {
            var visibilityOptions = from GroupVisibilityEnum e in Enum.GetValues(typeof(GroupVisibilityEnum))
                                    select new {
                                        ID = (int)e,
                                        Name = e.ToString()
                                    };
            var authorityOptions = from AuthorityEnum e in Enum.GetValues(typeof(AuthorityEnum))
                                    select new {
                                        ID = (int)e,
                                        Name = e.ToString()
                                    };

            ViewData["Visibility"] = new SelectList(visibilityOptions, "ID", "Name");
            ViewData["Authority"] = new SelectList(authorityOptions, "ID", "Name");

            return Page();
        }

        [BindProperty]
        public Group Group { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid || Group == null) {
                return Page();
            }

            var userId = _claimService.GetCurrentUserId;
            Group.CreationDate = DateTime.Now;
            Group.AccountCreatedID = userId;

            await _unitOfWork.GroupRepository.CreateGroupAsync(Group);

            await _unitOfWork.AccountInGroupRepository.AddAsync(new AccountInGroup
            {
                GroupId = Group.Id,
                AccountId = userId,
                Role = RoleEnum.Admin,
                Status = GroupStatusEnum.Active
            }); ;

            return  RedirectToPage($"/Groups/Details", new { id = Group.Id });
        }
    }
}
