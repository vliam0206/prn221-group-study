using Domain.Entities.Posts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Groups;

public class Group : BaseEntity
{
    #region Properties
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public GroupVisibilityEnum Visibility { get; set; }
    public bool ForceApprove { get; set; } = true;
    public AuthorityEnum ApproveAuthority { get; set; } = AuthorityEnum.Admin;
    public AuthorityEnum BanAuthority { get; set; } = AuthorityEnum.Admin;
    #endregion

    #region DB Relationship
    public ICollection<AccountInGroup> AccountInGroups { get; set; } = new HashSet<AccountInGroup>();
    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    #endregion
}
