using Domain.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.Groups {
    public interface IAccountInGroupRepository : IGenericRepository<AccountInGroup> {
        Task<AccountInGroup?> GetAccountInGroupAsync(Guid accountId, Guid groupId);
    }
}
