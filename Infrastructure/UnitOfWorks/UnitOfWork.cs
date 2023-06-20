using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.IRepositories.Groups;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork {
    #region Fields
    private readonly IAccountRepository _accountRepository;
    private readonly IGroupRepository _groupRepository;
    #endregion

    public UnitOfWork(IAccountRepository accountRepository, IGroupRepository groupRepository) {
        _accountRepository = accountRepository;
        _groupRepository = groupRepository;
    }

    public IAccountRepository AccountRepository => _accountRepository;

    public IGroupRepository GroupRepository => _groupRepository;
}
