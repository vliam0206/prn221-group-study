using Application.Commons;
using Application.IServices;
using DataAccess;
using Domain.Entities.Groups;
using Domain.Enums;
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
            var list = _dbContext.AccountInGroups.Where(x => x.GroupId == groupId).Include(x => x.Account).OrderByDescending(x => x.CreationDate);
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

        public async Task<Pagination<AccountInGroup>?> GetAccountListInGroupPaginationSearchAsync(Guid groupId, int pageIndex, int pageSize, string searchValue) {
            var list = _dbContext.AccountInGroups.Where(x => x.GroupId == groupId).Include(x => x.Account)
                .Where(x => x.Account.Username.Contains(searchValue) || x.Account.Email.Contains(searchValue))
                .OrderByDescending(x => x.CreationDate);
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

        public async Task<bool> AddAccountInGroupAsync(string username, Guid groupId) {
            var account = _dbContext.Accounts.Where(x => x.Username == username).FirstOrDefault();
            if (account == null) {
                return false;
            } else {
                if (_dbContext.AccountInGroups.Where(x => x.GroupId == groupId && x.AccountId == account.Id).Any())
                    return false;

                AccountInGroup accountInGroup = new AccountInGroup {
                    Role = Domain.Enums.RoleEnum.Member,
                    AccountId = account.Id,
                    GroupId = groupId
                };
                await _dbContext.AccountInGroups.AddAsync(accountInGroup);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task RemoveAccountInGroupAsync(Guid accountId, Guid groupId) {
            var account = _dbContext.AccountInGroups.Where(x => x.AccountId == accountId && x.GroupId ==  groupId).FirstOrDefault();
            if (account != null) {
                _dbContext.AccountInGroups.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task ChangeRoleAccountInGroupAsync(Guid accountId, Guid groupId, RoleEnum role) {
            var account = _dbContext.AccountInGroups.Where(x => x.AccountId == accountId && x.GroupId == groupId).FirstOrDefault();
            if (account != null) {
                account.Role = role;
                _dbContext.AccountInGroups.Update(account);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
