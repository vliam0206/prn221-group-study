using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;

public class TagInPost : BaseEntity
{
    #region Properties
    public Guid TagID { get; set; }
    public Guid PostId { get; set; }
    #endregion

    #region public Guid GroupId { get; set; }
    public Tag Tag { get; set; } = null!;    
    public Post Post { get; set; } = null!;
    #endregion
}
