using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;

public class Like : BaseEntity
{
    #region Properties
    public Guid PostId { get; set; }
    public LikeStatusEnum Status { get; set; } = LikeStatusEnum.Like;
    #endregion

    #region DB Relationship
    public Post Post { get; set; } = null!;
    public Account AccountCreated { get; set; } = null!;
    #endregion
}

