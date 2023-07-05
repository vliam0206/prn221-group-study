using Application.Commons;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
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
        public async Task<IActionResult> OnGetAsync(Guid groupId) {
            Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, 0, 10);
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string username, Guid groupId) {
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }

            bool check = await _unitOfWork.AccountInGroupRepository.AddAccountInGroupAsync(username, groupId);
            if (check) {
                Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, 0, 10);
                return Page();
            } else {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid accountId, Guid groupId) {
            await _unitOfWork.AccountInGroupRepository.RemoveAccountInGroupAsync(accountId, groupId);
            Accounts = await _unitOfWork.AccountInGroupRepository.GetAccountListInGroupPaginationAsync(groupId, 0, 10);
            GroupObj = await _unitOfWork.GroupRepository.GetByIdAsync(groupId);
            if (GroupObj == null) {
                return NotFound();
            }
            return Page();
        }
    }
}
