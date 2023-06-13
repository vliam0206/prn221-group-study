using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories;

public interface IAccountRepository
{
    public Task<bool> LoginAsync(string username, string password);
    public Task<Account?> GetAccountAsync(string username);
}
