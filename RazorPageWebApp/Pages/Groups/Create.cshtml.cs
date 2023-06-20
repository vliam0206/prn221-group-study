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

namespace RazorPageWebApp.Pages.Groups
{
    public class CreateModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContext;
        private IUnitOfWork _unitOfWork;

        public CreateModel(IHttpContextAccessor httpContext, IUnitOfWork unitOfWork) {
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Group == null)
            {
                return Page();
            }

            await _unitOfWork.GroupRepository.CreateGroupAsync(Group);

            return RedirectToPage("./Index");
        }
    }
}
