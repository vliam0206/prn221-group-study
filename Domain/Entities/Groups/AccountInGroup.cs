﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Groups;

public class AccountInGroup : BaseEntity
{
    #region Properties
    public RoleEnum Role { get; set; } = RoleEnum.Member;
    public GroupStatusEnum Status { get; set; } = GroupStatusEnum.Active;

    [DisplayName("Account ID")]
    public Guid AccountId { get; set; }

    [DisplayName("Group ID")]
    public Guid GroupId { get; set; }    
    #endregion

    #region DB Relationship
    public Account Account { get; set; } = null!;
    public Group Group { get; set; } = null!;
    #endregion
}
