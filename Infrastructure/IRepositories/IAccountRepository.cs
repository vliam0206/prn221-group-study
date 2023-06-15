using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(string username);

    Task InsertAccountAsync(Account account);
}
