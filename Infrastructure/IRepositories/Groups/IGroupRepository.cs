using Domain.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories.Groups; 
public interface IGroupRepository  :IGenericRepository<Group>
{
    Task<Group?> GetGroupByIdAsync(Guid? id);
    Task<List<Group>> GetAllGroupsAsync();
    Task CreateGroupAsync(Group group);
    Task UpdateGroupAsync(Group group);
    Task DeleteGroupAsync(Guid? id);
    Task<bool> IsUserInGroup(Guid userId, Guid groupId);
}
