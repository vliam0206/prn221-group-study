using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Groups;

public class AccountInGroup : BaseEntity
{
    #region Properties
    public RoleEnum Role { get; set; }
    public GroupStatusEnum Status { get; set; } = GroupStatusEnum.Active;
    public Guid AccountId { get; set; }    
    public Guid GroupId { get; set; }    
    #endregion

    #region DB Relationship
    public Account Account { get; set; } = null!;
    public Group Group { get; set; } = null!;
    #endregion
}
