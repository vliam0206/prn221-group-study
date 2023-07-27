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
    // check for the user if they have liked(unliked) this post before
    Task<bool> IsUserLiked(Guid postId, Guid userId);
}
