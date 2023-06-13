using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Groups;
using Infrastructure.Repositories.Posts;
using Microsoft.Extensions.Options;
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
    private readonly PostRepository _postRepository;
    private readonly GroupRepository _groupRepository;
    #endregion
    public UnitOfWork(IAccountRepository accountRepository,PostRepository postRepository,GroupRepository groupRepository)
    {
        _accountRepository = accountRepository;
       _postRepository = postRepository;
        _groupRepository = groupRepository;
    }
    public IAccountRepository AccountRepository => _accountRepository;

    public GroupRepository GroupRepository => _groupRepository;

    public PostRepository PostRepository => _postRepository;
}
