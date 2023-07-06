using Application.Commons;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.Groups {
    public interface IAccountInGroupRepository : IGenericRepository<AccountInGroup> {
        Task<AccountInGroup?> GetAccountInGroupAsync(Guid accountId, Guid groupId);
        Task<Pagination<AccountInGroup>?> GetAccountListInGroupPaginationAsync(Guid groupId, int pageIndex, int pageSize);
        Task<Pagination<AccountInGroup>?> GetAccountListInGroupPaginationSearchAsync(Guid groupId, int pageIndex, int pageSize, string searchValue);
        Task<bool> AddAccountInGroupAsync(string username, Guid groupId);

        Task RemoveAccountInGroupAsync(Guid accountId, Guid groupId);
    }
}
