﻿using System;
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

namespace RazorPageWebApp.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContext;
        private IUnitOfWork _unitOfWork;

        public DetailsModel(IHttpContextAccessor httpContext, IUnitOfWork unitOfWork) {
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public Group Group { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            else 
            {
                Group = group;
            }
            return Page();
        }
    }
}
