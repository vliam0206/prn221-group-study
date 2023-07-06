using Application.Commons;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
using Domain.Enums;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApp.Pages.Groups {
    public class MembersModel : PageModel {
        private readonly IUnitOfWork _unitOfWork;

        public MembersModel(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public Pagination<AccountInGroup>? Accounts { get; set; }
        public Group? GroupObj { get; set; }

        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync(int? pageIndex, Guid groupId, string? searchValue) {
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }

            int index = 0;
            if (pageIndex != null) {
                index = pageIndex.Value;
            }

            if (searchValue == null) {
                Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, index, 10);
            } else {
                Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationSearchAsync(groupId, index, 10, searchValue);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(string username, Guid groupId) {
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }

            bool check = await _unitOfWork.AccountInGroupRepository.AddAccountInGroupAsync(username, groupId);
            Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, 0, 10);

            if (check) {
                Message = "Add member successfully";
                return Page();
            } else {
                Message = "Add member failed";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid accountId, Guid groupId) {
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }

            await _unitOfWork.AccountInGroupRepository.RemoveAccountInGroupAsync(accountId, groupId);
            Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, 0, 10);
            
            return Page();
        }

        public async Task<IActionResult> OnPostChangeRoleAsync(Guid accountId, Guid groupId, RoleEnum role) {
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }

            await _unitOfWork.AccountInGroupRepository.ChangeRoleAccountInGroupAsync(accountId, groupId, role);
            Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, 0, 10);

            return Page();
        }
    }
}
