using DataAccess;
using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    public async Task<bool> LoginAsync(string username, string password)
    {
        var account = await GetAccountAsync(username);
        if (account == null || !account.Password.Equals(password))
        {
            return false;
        }
        return true;
    }

    public async Task<Account?> GetAccountAsync(string username)
    {
        var dbContext = new AppDBContext();
        var account =  await dbContext.Accounts.FirstOrDefaultAsync(x => x.Username == username);
        return account;
    }

}
