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
using Application.Commons;

namespace RazorPageWebApp.Pages.Groups;
public class IndexModel : PageModel {
    private IUnitOfWork _unitOfWork;

    public IndexModel(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public Pagination<Group> Groups { get; set; }
    public String SearchString { get; set; }

    public async Task OnGetAsync(int? pageIndex, string? searchValue)
    {
        var index = 0;
        if (pageIndex != null)
        {
            index = pageIndex.Value;
        }
        if (string.IsNullOrEmpty(searchValue))
        {
            Groups = await _unitOfWork.GroupRepository.ToPagination(index, AppConstants.GROUP_PAGE_SIZE);
        } else
        {
            SearchString = searchValue;
            Groups = await _unitOfWork.GroupRepository.SearchGroupPaginAsync(index, AppConstants.GROUP_PAGE_SIZE, searchValue);
        }
        
    }
}
