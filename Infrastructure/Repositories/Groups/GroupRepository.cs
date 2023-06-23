using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Groups;

public class GroupRepository
{
    private readonly AppDBContext _context;

    public GroupRepository(AppDBContext context)
    {
        this._context = context;
    }

    public async Task<bool> IsUserInGroup(Guid userId,Guid groupId)
    {
        return await _context.AccountInGroups.AnyAsync(x => x.AccountId == userId && x.GroupId == groupId);
    }
}
