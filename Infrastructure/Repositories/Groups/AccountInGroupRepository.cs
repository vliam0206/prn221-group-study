using Application.Commons;
using Application.IServices;
using DataAccess;
using Domain.Entities.Groups;
using Infrastructure.IRepositories.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Groups {
    public class AccountInGroupRepository : GenericRepository<AccountInGroup>, IAccountInGroupRepository {
        private readonly AppDBContext _dbContext;

        public AccountInGroupRepository(IClaimService claimService):base(claimService) {
            _dbContext = new AppDBContext();
        }
        public async Task<AccountInGroup?> GetAccountInGroupAsync(Guid accountId, Guid groupId) {
            return await _dbContext.AccountInGroups.FirstOrDefaultAsync(x => x.AccountId == accountId && x.GroupId == groupId);

        }
        
        public async Task<Pagination<AccountInGroup>?> GetAccountListInGroupPaginationAsync(Guid groupId, int pageIndex, int pageSize) {
            var list = _dbContext.AccountInGroups.Where(x => x.GroupId == groupId).Include(x => x.Account);
            var itemCount = await list.CountAsync();
            var accounts = await list.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var pagination = new Pagination<AccountInGroup> {
                TotalItemsCount = itemCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                Items = accounts
            };
            return pagination;
        }
    }
}
