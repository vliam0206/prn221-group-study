using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<Account?> GetAccountByUsernameAsync(string username);

    Task InsertAccountAsync(Account account);
    // get the like data recently also get unlike data
    Task<bool> IsUserLiked(Guid postId, Guid userId);
}
