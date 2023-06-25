﻿using DataAccess;
using Domain.Entities.Groups;
using Infrastructure.IRepositories.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Groups;

public class GroupRepository : GenericRepository<Group>, IGroupRepository {
    private readonly AppDBContext _dbcontext;

    public GroupRepository() {
        _dbcontext = new AppDBContext();
    }

    public async Task CreateGroupAsync(Group group) {
        _dbcontext.Groups.Add(group);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteGroupAsync(Guid? id) {
        var group = await _dbcontext.Groups.FindAsync(id);
        if (group == null)
            return;

        _dbcontext.Groups.Remove(group);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<List<Group>> GetAllGroupsAsync() {
        return await _dbcontext.Groups.ToListAsync();
    }

    public async Task<Group?> GetGroupByIdAsync(Guid? id) {
        return await _dbcontext.Groups.FindAsync(id);
    }

    public async Task UpdateGroupAsync(Group group) {
        _dbcontext.Groups.Update(group);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<bool> IsUserInGroup(Guid userId, Guid groupId) {
        return await _dbcontext.AccountInGroups.AnyAsync(x => x.AccountId == userId && x.GroupId == groupId);
    }
}
