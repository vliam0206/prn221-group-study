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

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    private AppDBContext _dbContext;

    public AccountRepository()
    {
        _dbContext = new AppDBContext();
    }    
    public async Task<Account?> GetAccountByUsernameAsync(string username)
        => await _dbContext.Accounts.SingleOrDefaultAsync(x => x.Username == username);

    public async Task<Account?> GetAccountByEmailAsync(string email)
        => await _dbContext.Accounts.SingleOrDefaultAsync(x => x.Email == email);

    public async Task InsertAccountAsync(Account account)
    {
        var acc1 = await GetAccountByUsernameAsync(account.Username);
        var acc2 = await GetAccountByEmailAsync(account.Email);
        if (acc1 != null)
        {
            throw new Exception("Duplicated username!");
        }
        if (acc2 != null)
        {
            throw new Exception("Duplicated email!");
        }
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
    }        
}
