using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Domain.Entities.Groups;
using Infrastructure.IRepositories.Groups;
using Infrastructure.UnitOfWorks;
using Application.IServices;
using Domain.Enums;

namespace RazorPageWebApp.Pages.Groups
{
    public class EditModel : PageModel
    {
        private readonly IClaimService _claimService;
        private IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork, IClaimService claimService)
        {            
            _unitOfWork = unitOfWork;
            _claimService = claimService;
        }
        [BindProperty]
        public Group Group { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Group = await _unitOfWork.GroupRepository.GetByIdAsync(id);
            var visibilityOptions = from GroupVisibilityEnum e in Enum.GetValues(typeof(GroupVisibilityEnum))
                                    select new
                                    {
                                        ID = (int)e,
                                        Name = e.ToString()
                                    };
            var authorityOptions = from AuthorityEnum e in Enum.GetValues(typeof(AuthorityEnum))
                                   select new
                                   {
                                       ID = (int)e,
                                       Name = e.ToString()
                                   };

            ViewData["Visibility"] = new SelectList(visibilityOptions, "ID", "Name");
            ViewData["Authority"] = new SelectList(authorityOptions, "ID", "Name");

            return Page();
        }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Group == null)
            {
                return Page();
            }
            Group.ModificationDate = DateTime.Now;            
            Group.AccountCreatedID = _claimService.GetCurrentUserId;

            await _unitOfWork.GroupRepository.UpdateAsync(Group);

            return RedirectToPage($"/Groups/Details", new { id = Group.Id });
        }
    }
}
