﻿using Infrastructure.IRepositories;
using Infrastructure.IRepositories.Groups;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks;

public interface IUnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public IGroupRepository  GroupRepository { get; }
}
