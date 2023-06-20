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

namespace RazorPageWebApp.Pages.Groups {
    public class IndexModel : PageModel {
        private readonly IHttpContextAccessor _httpContext;
        private IUnitOfWork _unitOfWork;

        public IndexModel(IHttpContextAccessor httpContext, IUnitOfWork unitOfWork) {
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
        }

        public IList<Group> Group { get; set; } = default!;

        public async Task OnGetAsync() {
            Group = await _unitOfWork.GroupRepository.GetAllGroupsAsync();
        }
    }
}
