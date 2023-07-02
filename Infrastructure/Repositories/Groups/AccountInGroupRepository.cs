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

        public AccountInGroupRepository() {
            _dbContext = new AppDBContext();
        }
        public async Task<AccountInGroup?> GetAccountInGroupAsync(Guid accountId, Guid groupId) {
            return await _dbContext.AccountInGroups.FirstOrDefaultAsync(x => x.AccountId == accountId && x.GroupId == groupId);

        }
    }
}
