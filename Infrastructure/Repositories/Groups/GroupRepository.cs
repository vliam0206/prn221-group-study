using DataAccess;
using Application.Commons;
using DataAccess;
using Domain.Entities.Groups;
using Infrastructure.IRepositories.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Groups;

public class GroupRepository : GenericRepository<Group>, IGroupRepository
{
    private readonly AppDBContext _dbcontext;

    public GroupRepository()
    {
        _dbcontext = new AppDBContext();
    }

    public async Task CreateGroupAsync(Group group)
    {
        _dbcontext.Groups.Add(group);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteGroupAsync(Guid? id)
    {
        var group = await _dbcontext.Groups.FindAsync(id);
        if (group == null)
            return;

        _dbcontext.Groups.Remove(group);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<Group>> GetAllGroupsAsync()
    {
        return await _dbcontext.Groups.Include(x => x.Posts).ToListAsync();
    }

    public async Task<Group?> GetGroupByIdAsync(Guid? id)
    {
        return await _dbcontext.Groups.Include(x => x.Posts).ThenInclude(x=>x.AccountCreated).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateGroupAsync(Group group)
    {
        _dbcontext.Groups.Update(group);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<bool> IsUserInGroup(Guid userId, Guid groupId)
    {
        return await _dbcontext.AccountInGroups.AnyAsync(x => x.AccountId == userId && x.GroupId == groupId);
    }

    public async Task<List<Group>> GetGroupsAsync(Guid userId)
    {
        return await _dbcontext.AccountInGroups
                            .Include(x => x.Group)
                            .Where(x => x.AccountId == userId)
                            .Select(x => x.Group)
                            .ToListAsync();
    }

    public async Task<Pagination<Group>> GetGroupsToPaginAsync(Guid userId, int pageIndex, int pageSize)
    {
        var groupList = _dbcontext.AccountInGroups
                            .Include(x => x.Group)
                            .Where(x => x.AccountId == userId)
                            .Select(x => x.Group);
        var itemCount = await groupList.CountAsync();
        var items = await groupList.Skip(pageIndex * pageSize)
                              .Take(pageSize)
                              .AsNoTracking()
                              .ToListAsync();
        return new Pagination<Group>
        {
            Items = items,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItemsCount = itemCount
        };
    }

    public async Task<List<Group>> GetTopGroupsAsync(int limitNum)
    {
        return await _dbcontext.Groups
                        .Take(limitNum)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<List<Group>> GetTopGroupsAsync(Guid userId, int limitNum)
    {
        return await _dbcontext.AccountInGroups
                            .Include(x => x.Group)
                            .Where(x => x.AccountId == userId)
                            .Select(x => x.Group)
                            .Take(limitNum)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<Pagination<Group>> SearchGroupPaginAsync(int pageIndex, int pagesize, string searchValue)
    {
        var itemCount = await _dbcontext.Groups.CountAsync();
        var items = await _dbcontext.Groups
                              .Where(x => x.Name.Contains(searchValue)
                                        || x.Description!.Contains(searchValue))
                              .Skip(pageIndex * pagesize)
                              .Take(pagesize)
                              .AsNoTracking()
                              .ToListAsync();
        return new Pagination<Group>
        {
            Items = items,
            PageIndex = pageIndex,
            PageSize = pagesize,
            TotalItemsCount = itemCount
        };
    }
}
