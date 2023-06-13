using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    #region Fields
    private readonly IAccountRepository _accountRepository;
    #endregion
    public UnitOfWork(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public IAccountRepository AccountRepository => _accountRepository;
}
