﻿using Infrastructure.IRepositories;
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
}
